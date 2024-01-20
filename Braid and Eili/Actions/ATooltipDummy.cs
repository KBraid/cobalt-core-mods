using HarmonyLib;
using System;
using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;
public class ATooltipDummy : ADummyAction
{
    public List<Tooltip>? tooltips;
    public List<Icon>? icons;

    public static ATooltipDummy BuildStandIn(CardAction action, State s)
    {
        if (action is AAttack aattack)
        {
            int cannonX = s.ship.parts.FindIndex((Part p) => p.type == PType.cannon && p.active);
            return BuildFromAttack(aattack, s, cannonX: cannonX);
        }

        if (action is AStatus astatus)
        {
            return BuildFromStatus(astatus, s);
        }

        Icon? icon = action.GetIcon(s);
        return new ATooltipDummy()
        {
            tooltips = action.GetTooltips(s),
            icons = icon == null ? new() : new() { (Icon)action.GetIcon(s)! }
        };
    }

    public static ATooltipDummy BuildFromStatus(AStatus astatus, State s)
    {
        List<Icon> icons = new();

        if (!astatus.targetPlayer)
        {
            icons.Add(new Icon(StableSpr.icons_outgoing, null, Colors.textMain));
        }

        Icon? icon = astatus.GetIcon(s);
        if (icon != null)
        {
            icons.Add((Icon)icon);
        }


        return new ATooltipDummy()
        {
            tooltips = astatus.GetTooltips(s),
            icons = icons
        };
    }

    public static ATooltipDummy BuildFromAttack(AAttack aattack, State s, int? cannonX = null, bool hideOutgoingArrow = true)
    {
        List<Icon> icons = new();
        var tooltips = aattack.GetTooltips(s);

        icons.Add(new Icon(StableSpr.icons_attack, aattack.damage, Colors.redd));

        if (aattack.stunEnemy)
        {
            icons.Add(new Icon(StableSpr.icons_stun, null, Colors.textMain));
        }

        if (aattack.status != null)
        {
            // this cast is ridiculous. It's needed because C# still thinks aattack.status can be null, even though it must be non-null to get here
            var icon = new AStatus()
            {
                status = (Status)aattack.status,
                targetPlayer = hideOutgoingArrow,
                statusAmount = aattack.statusAmount
            }.GetIcon(s);

            if (icon != null)
            {
                if (!hideOutgoingArrow) icons.Add(new Icon(StableSpr.icons_outgoing, null, Colors.textMain));
                icons.Add((Icon)icon);
            }
        }

        if (aattack.moveEnemy != 0)
        {
            Spr spr = aattack.moveEnemy < 0
                ? StableSpr.icons_moveLeftEnemy
                : StableSpr.icons_moveRightEnemy;
            icons.Add(new Icon(spr, Math.Abs(aattack.moveEnemy), Colors.redd));
        }

        return new ATooltipDummy()
        {
            tooltips = tooltips,
            icons = icons
        };
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        return tooltips ?? new();
    }

    public override Icon? GetIcon(State s)
    {
        return null;
    }
}
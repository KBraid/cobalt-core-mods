using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBraid.BraidEili.Actions;

public class ATempBrittleAttack : AAttackNoIcon
{
    public override void Begin(G g, State s, Combat c)
    {
        Ship ship = (targetPlayer ? s.ship : c.otherShip);
        Ship ship2 = (targetPlayer ? c.otherShip : s.ship);
        if (ship == null || ship2 == null || ship.hull <= 0 || (fromDroneX.HasValue && !c.stuff.ContainsKey(fromDroneX.Value)))
        {
            return;
        }

        int? num = GetFromX(s, c);
        RaycastResult? raycastResult;
        if (base.fromDroneX.HasValue)
        {
            raycastResult = ((RaycastResult?)CombatUtils.RaycastGlobal(c, ship, fromDrone: true, fromDroneX.Value));
        }
        else
        {
            raycastResult = ((num.HasValue ? CombatUtils.RaycastFromShipLocal(s, c, num.Value, targetPlayer) : null));
        }
        base.Begin(g, s, c);
        if (raycastResult != null && raycastResult.hitShip)
        {
            c.QueueImmediate(new ATempBrittlePart
            {
                TargetPlayer = targetPlayer,
                WorldX = raycastResult.worldX,
                omitFromTooltips = true
            });
        }
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> list = base.GetTooltips(s);
        list.Add(
            new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.parttrait,
                () => GetIcon(),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempBrittle", "name"]),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempBrittle", "description"]))
        );
        return list;
    }
    public static Spr? GetIcon()
    {
        return ModEntry.Instance.AApplyTempBrittle_Icon.Sprite;
    }
}

using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;

internal class AAttackRandomMove : AAttackNoIcon
{
    public int randomDir;
    public override void Begin(G g, State s, Combat c)
    {
        Ship target = (targetPlayer ? s.ship : c.otherShip);
        Ship source = (targetPlayer ? c.otherShip : s.ship);
        if (target == null || source == null || target.hull <= 0 || (fromDroneX.HasValue && !c.stuff.ContainsKey(fromDroneX.Value)))
        {
            return;
        }

        int? num = GetFromX(s, c);
        RaycastResult? raycastResult;
        if (fromDroneX.HasValue)
        {
            raycastResult = ((RaycastResult?)CombatUtils.RaycastGlobal(c, target, fromDrone: true, fromDroneX.Value));
        }
        else
        {
            raycastResult = ((num.HasValue ? CombatUtils.RaycastFromShipLocal(s, c, num.Value, targetPlayer) : null));
        }
        bool flag = true;
        if (target.Get(Status.autododgeLeft) > 0 || target.Get(Status.autododgeRight) > 0)
            flag = false;
        base.Begin(g, s, c);
        if (raycastResult != null && raycastResult.hitShip && flag)
        {
            c.QueueImmediate(new AMove()
            {
                dir = Extensions.GetRandomDirection(s, randomDir),
                targetPlayer = targetPlayer,
                omitFromTooltips = true,
            });
        }
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> list = base.GetTooltips(s);
        list.Add(new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.action,
                () => GetIcon(),
                () => ModEntry.Instance.Localizations.Localize(["action", "AAttackRandomMove", "name"]),
                () => ModEntry.Instance.Localizations.Localize(["action", "AAttackRandomMove", "description"]))
        );
        return list;
    }
    public static Spr? GetIcon()
    {
        return ModEntry.Instance.ARandomMove.Sprite;
    }
}

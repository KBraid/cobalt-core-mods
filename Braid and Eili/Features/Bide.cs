using HarmonyLib;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class BideManager : IStatusLogicHook
{
    public BideManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.NormalDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Prefix)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Postfix))
        );

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Postfix))
        );
    }
    public bool? IsAffectedByBoost(State state, Combat combat, Ship ship, Status status)
    {
        if (status != ModEntry.Instance.Bide.Status)
            return null;
        return false;
    }
    private static void Ship_NormalDamage_Prefix(
        Ship __instance,
        State s,
        Combat c,
        int incomingDamage,
        bool piercing,
        ref int __state)
    {
        if (__instance.Get(ModEntry.Instance.Bide.Status) <= 0)
            return;
        var ship = __instance.isPlayerShip ? s.ship : c.otherShip;
        if (piercing || ship.Get(Status.shield) + ship.Get(Status.tempShield) < incomingDamage)
        {
            __state = ship.Get(Status.shield) + ship.Get(Status.tempShield);
        }
        else
        {
            __state = 0;
        }
        return;
    }
    private static void Ship_NormalDamage_Postfix(
        Ship __instance,
        State s,
        Combat c,
        int incomingDamage,
        int? maybeWorldGridX,
        bool piercing,
        ref int __state)
    {
        if (__instance.Get(ModEntry.Instance.Bide.Status) <= 0)
            return;
        object? obj;
        if (maybeWorldGridX.HasValue)
        {
            int valueOrDefault = maybeWorldGridX.GetValueOrDefault();
            obj = __instance.GetPartAtWorldX(valueOrDefault);
        }
        else
        {
            obj = null;
        }

        Part? part = (Part?)obj;
        if (__instance.isPlayerShip)
        {
            foreach (Artifact item in s.EnumerateAllArtifacts())
            {
                item.OnPlayerTakeNormalDamage(s, c, incomingDamage, part);
            }
        }

        int num = incomingDamage;
        if (part != null)
        {
            num = __instance.ModifyDamageDueToParts(s, c, incomingDamage, part, piercing);
        }
        if (num < incomingDamage)
            num = incomingDamage;
        if (num > 0)
        {
            var ship = __instance.isPlayerShip ? s.ship : c.otherShip;
            var bide_status = ModEntry.Instance.Bide.Status;
            ship.PulseStatus(bide_status);
            if (__state == 0)
            {
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.PerfectTiming.Status,
                    statusAmount = num,
                    targetPlayer = ship.isPlayerShip
                });
            }
            else
            {
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.PerfectTiming.Status,
                    statusAmount = __state,
                    targetPlayer = ship.isPlayerShip
                });
            }
        }
        return;
    }
    private static void Ship_DirectHullDamage_Postfix(
        Ship __instance,
        State s,
        Combat c,
        int amt)
    {
        if (__instance.hull <= 0 || amt <= 0 || __instance.Get(Status.perfectShield) > 0)
            return;
        if (__instance.Get(ModEntry.Instance.Bide.Status) <= 0)
            return;
        var ship = __instance.isPlayerShip ? s.ship : c.otherShip;
        var bide_status = ModEntry.Instance.Bide.Status;
        ship.PulseStatus(bide_status);
        c.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.PerfectTiming.Status,
            statusAmount = ship.Get(bide_status),
            targetPlayer = ship.isPlayerShip
        });
        return;
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.Bide.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.PerfectTiming.Status, 1)).ToList();
    }
}
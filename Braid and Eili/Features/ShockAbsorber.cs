using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class ShockAbsorberManager : IStatusLogicHook
{
    public ShockAbsorberManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.NormalDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Prefix))
        );

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Prefix))
        );
    }
    private static void Ship_NormalDamage_Prefix(
        Ship __instance,
        State s,
        Combat c,
        int incomingDamage,
        int? maybeWorldGridX,
        bool piercing = false)
    {
        if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) <= 0)
            return;

        int num = incomingDamage;
        if (maybeWorldGridX.HasValue)
        {
            int valueOrDefault = maybeWorldGridX.GetValueOrDefault();
            Part? part = __instance.GetPartAtWorldX(valueOrDefault);
            if (part != null)
            {
                num = __instance.ModifyDamageDueToParts(s, c, incomingDamage, part, piercing);
            }
        }
        if (num == 0)
            return;
        if (!piercing)
        {
            int num2 = __instance.Get(Status.shield) + __instance.Get(Status.tempShield);
            if (num2 <= 0)
                return;
            int num3 = num - num2;
            int num4 = num;
            if (num3 > 0)
                num4 = num3;
            else if (num3 < 0)
                num4 = -1 * num3;
            if (num4 != 0)
            {
                var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
                __instance.PulseStatus(shockAbsorber);
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.TempShieldNextTurn.Status,
                    statusAmount = num4 * __instance.Get(shockAbsorber),
                    targetPlayer = __instance.isPlayerShip
                });
            }
        }
    }
    private static void Ship_DirectHullDamage_Prefix(
        Ship __instance,
        Combat c,
        int amt)
    {
        if (__instance.hull <= 0 || amt <= 0 || __instance.Get(Status.perfectShield) > 0)
            return;
        if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) <= 0)
            return;
        var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
        __instance.PulseStatus(shockAbsorber);
        c.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.TempShieldNextTurn.Status,
            statusAmount = amt * __instance.Get(shockAbsorber),
            targetPlayer = __instance.isPlayerShip
        });
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.ShockAbsorber.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.TempShieldNextTurn.Status, 1)).ToList();
    }
}
using HarmonyLib;
using System.Collections.Generic;

namespace KBraid.BraidEili;

internal sealed class ShipPatchesManager
{
    public ShipPatchesManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.NormalDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Prefix)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_NormalDamage_Postfix))
        );
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Prefix)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Postfix))
        );
    }
    private static void Ship_NormalDamage_Prefix(
        Ship __instance,
        bool piercing,
        ref int __state)
    {
        if (!piercing)
        {
            __state = __instance.Get(Status.shield) + __instance.Get(Status.tempShield);
        }
        else
        {
            __state = 0;
        }
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
        int num = incomingDamage;
        if (part != null)
        {
            num = __instance.ModifyDamageDueToParts(s, c, incomingDamage, part, piercing);
        }
        var num2 = __state;
        if (num < __state)
            num2 = num;
        if (!piercing && num2 != 0)
        {
            if (__instance.Get(ModEntry.Instance.Bide.Status) > 0)
            {
                var bide_status = ModEntry.Instance.Bide.Status;
                __instance.PulseStatus(bide_status);
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.PerfectTiming.Status,
                    statusAmount = num2,
                    targetPlayer = __instance.isPlayerShip
                });
            }
            if (__instance.Get(ModEntry.Instance.DisabledDampeners.Status) > 0)
            {
                var disabledDampeners = ModEntry.Instance.DisabledDampeners.Status;
                __instance.PulseStatus(disabledDampeners);
                c.QueueImmediate(new AStatus()
                {
                    timer = 0,
                    status = Status.evade,
                    statusAmount = num2 * __instance.Get(disabledDampeners),
                    targetPlayer = __instance.isPlayerShip
                });
                c.QueueImmediate(new AMove()
                {
                    dir = num2 * __instance.Get(disabledDampeners),
                    isRandom = true,
                    targetPlayer = __instance.isPlayerShip
                });
            }
            if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) > 0)
            {
                var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
                __instance.PulseStatus(shockAbsorber);
                c.QueueImmediate(new AStatus()
                {
                    status = ModEntry.Instance.TempShieldNextTurn.Status,
                    statusAmount = num2 * __instance.Get(shockAbsorber),
                    targetPlayer = __instance.isPlayerShip
                });
            }
        }
    }
    private static void Ship_DirectHullDamage_Prefix(
        Ship __instance,
        ref int amt,
        ref Dictionary<string, int> __state)
    {
        __state = new Dictionary<string, int>
        {
            { "lostHull", __instance.hull }
        };
        if (__instance.hullMax > 1 && __instance.Get(ModEntry.Instance.Resolve.Status) > 0)
        {
            if (amt >= __instance.hull)
            {
                var num = amt - __instance.hull;
                if (num == 0)
                    num = 1;
                __state.Add("resolve", num);
                amt -= 1;
            }
        }
    }
    private static void Ship_DirectHullDamage_Postfix(
        Ship __instance,
        Combat c,
        ref int amt,
        ref Dictionary<string, int> __state)
    {
        if (__instance.Get(ModEntry.Instance.Resolve.Status) > 0)
        {
            if (__state.TryGetValue("resolve", out int value) && value > 0)
            {
                __instance.PulseStatus(ModEntry.Instance.Resolve.Status);
                __instance.hullMax -= value;
                if (__instance.hullMax <= 0)
                {
                    __instance.hull = 0;
                }
                else
                {
                    c.QueueImmediate(new AStatus()
                    {
                        timer = 0,
                        status = ModEntry.Instance.LostHull.Status,
                        statusAmount = value + __instance.Get(ModEntry.Instance.LostHull.Status),
                        mode = AStatusMode.Set,
                        targetPlayer = __instance.isPlayerShip,
                        dialogueSelector = __instance.isPlayerShip ? ".resolvetriggered" : null

                    });
                }
            }
        }
        if (__instance.hull <= 0 || amt <= 0)
            return;
        if (__state.TryGetValue("lostHull", out int value2) && value2 > 0)
        {
            c.QueueImmediate(new AStatus()
            {
                timer = 0,
                status = ModEntry.Instance.LostHull.Status,
                statusAmount = (value2 - __instance.hull) + __instance.Get(ModEntry.Instance.LostHull.Status),
                mode = AStatusMode.Set,
                targetPlayer = __instance.isPlayerShip
            });
        }
        if (__instance.Get(ModEntry.Instance.Bide.Status) > 0)
        {
            var bide_status = ModEntry.Instance.Bide.Status;
            __instance.PulseStatus(bide_status);
            c.QueueImmediate(new AStatus()
            {
                status = ModEntry.Instance.PerfectTiming.Status,
                statusAmount = amt,
                targetPlayer = __instance.isPlayerShip
            });
        }
        if (__instance.Get(ModEntry.Instance.DisabledDampeners.Status) > 0)
        {
            var disabledDampeners = ModEntry.Instance.DisabledDampeners.Status;
            __instance.PulseStatus(disabledDampeners);
            c.QueueImmediate(new AStatus()
            {
                status = Status.evade,
                statusAmount = amt * __instance.Get(disabledDampeners),
                targetPlayer = __instance.isPlayerShip
            });
            c.QueueImmediate(new AMove()
            {
                dir = amt * __instance.Get(disabledDampeners),
                isRandom = true,
                targetPlayer = __instance.isPlayerShip
            });
        }
        if (__instance.Get(ModEntry.Instance.ShockAbsorber.Status) > 0)
        {
            var shockAbsorber = ModEntry.Instance.ShockAbsorber.Status;
            __instance.PulseStatus(shockAbsorber);
            c.QueueImmediate(new AStatus()
            {
                status = ModEntry.Instance.TempShieldNextTurn.Status,
                statusAmount = amt * __instance.Get(shockAbsorber),
                targetPlayer = __instance.isPlayerShip
            });
        }
    }
}
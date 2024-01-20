using HarmonyLib;

namespace KBraid.BraidEili;
internal sealed class LostHullManager : IStatusLogicHook
{
    public LostHullManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);

        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(Ship), nameof(Ship.DirectHullDamage)),
            prefix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Prefix)),
            postfix: new HarmonyMethod(GetType(), nameof(Ship_DirectHullDamage_Postfix))
        );
    }
    public bool? IsAffectedByBoost(State state, Combat combat, Ship ship, Status status)
    {
        if (status != ModEntry.Instance.LostHull.Status)
            return null;
        return false;
    }
    private static void Ship_DirectHullDamage_Prefix(Ship __instance, ref int __state)
        => __state = __instance.hull;

    private static void Ship_DirectHullDamage_Postfix(Ship __instance, Combat c, ref int __state)
    {
        var damageTaken = __state - __instance.hull;
        if (damageTaken <= 0)
            return;
        c.QueueImmediate(new AStatus()
        {
            status = ModEntry.Instance.LostHull.Status,
            statusAmount = damageTaken,
            targetPlayer = __instance.isPlayerShip
        });
    }
}
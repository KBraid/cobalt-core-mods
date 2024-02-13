using FMOD;
using HarmonyLib;

namespace KBraid.BraidEili;

internal sealed class StatusMetaPatchesManager
{
    private static ModEntry Instance => ModEntry.Instance;
    public StatusMetaPatchesManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(StatusMeta), nameof(StatusMeta.GetSound)),
            postfix: new HarmonyMethod(GetType(), nameof(StatusMeta_GetSound_PostFix))
        );
    }
    private static void StatusMeta_GetSound_PostFix(
        Status status,
        bool isIncrease,
        ref GUID __result)
    {
        if (status == Instance.DisabledDampeners.Status || status == Instance.Bide.Status || status == Instance.BusterCharge.Status || status == Instance.PerfectTiming.Status)
        {
            __result = isIncrease ? FSPRO.Event.Status_EvadeUp : FSPRO.Event.Status_EvadeDown;
        }
        else if (status == Instance.TempShieldNextTurn.Status)
        {
            __result = isIncrease ? FSPRO.Event.Status_TempshieldUp : FSPRO.Event.Status_TempshieldDown;
        }
        else if (status == Instance.TempPowerdrive.Status)
        {
            __result = isIncrease ? FSPRO.Event.Status_PowerUp : new GUID() { Data1 = 0, Data2 = 0, Data3 = 0, Data4 = 0 };
        }
        else if (status == Instance.EngineStallNextTurn.Status)
        {
            __result = isIncrease ? FSPRO.Event.Status_PowerDown : new GUID() { Data1 = 0, Data2 = 0, Data3 = 0, Data4 = 0 };
        }
    }
}
using FMOD;
using HarmonyLib;
using Microsoft.Xna.Framework.Media;

namespace KBraid.BraidEili;
internal sealed class LostHullManager : IStatusLogicHook
{
    public LostHullManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool? IsAffectedByBoost(State state, Combat combat, Ship ship, Status status)
    {
        if (status != ModEntry.Instance.LostHull.Status)
            return null;
        return false;
    }
}
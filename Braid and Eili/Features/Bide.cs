using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class BideManager : IStatusLogicHook
{
    public BideManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool? IsAffectedByBoost(State state, Combat combat, Ship ship, Status status)
    {
        if (status != ModEntry.Instance.Bide.Status)
            return null;
        return false;
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.Bide.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.PerfectTiming.Status, 1)).ToList();
    }
}
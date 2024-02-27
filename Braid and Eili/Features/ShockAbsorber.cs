using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;
internal sealed class ShockAbsorberManager : IStatusLogicHook
{
    public ShockAbsorberManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public List<Tooltip> OverrideStatusTooltips(Status status, int amount, bool isForShipStatus, List<Tooltip> tooltips)
    {
        if (status != ModEntry.Instance.ShockAbsorber.Status)
            return tooltips;
        return tooltips.Concat(StatusMeta.GetTooltips(ModEntry.Instance.TempShieldNextTurn.Status, 1)).ToList();
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.ShockAbsorber.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            amount--;
        return false;
    }
}
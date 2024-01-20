using KBraid.BraidEili.Actions;
namespace KBraid.BraidEili;
internal sealed class RetreatManager : IStatusLogicHook
{
    public RetreatManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool? IsAffectedByBoost(State state, Combat combat, Ship ship, Status status)
    {
        if (status != ModEntry.Instance.Retreat.Status)
            return null;
        return false;
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.Retreat.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            combat.QueueImmediate(new ARetreat());
        return false;
    }
}
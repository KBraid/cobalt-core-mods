namespace KBraid.BraidEili;
internal sealed class EngineStallNextTurnManager : IStatusLogicHook
{
    public EngineStallNextTurnManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.EngineStallNextTurn.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
            combat.QueueImmediate(new AStatus()
            {
                status = Status.engineStall,
                statusAmount = amount,
                targetPlayer = ship.isPlayerShip,
            });
        amount = 0;
        return false;
    }
}
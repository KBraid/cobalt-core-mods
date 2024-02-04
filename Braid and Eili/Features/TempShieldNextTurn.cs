namespace KBraid.BraidEili;
internal sealed class TempShieldNextTurnManager : IStatusLogicHook
{
    public TempShieldNextTurnManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
    public bool HandleStatusTurnAutoStep(State state, Combat combat, StatusTurnTriggerTiming timing, Ship ship, Status status, ref int amount, ref StatusTurnAutoStepSetStrategy setStrategy)
    {
        if (status != ModEntry.Instance.TempShieldNextTurn.Status)
            return false;
        if (timing != StatusTurnTriggerTiming.TurnStart)
            return false;

        if (amount > 0)
        {
            combat.QueueImmediate(new AStatus()
            {
                status = Status.tempShield,
                statusAmount = amount,
                targetPlayer = ship.isPlayerShip,
            });
            combat.QueueImmediate(new AStatus()
            {
                status = status,
                statusAmount = 0,
                mode = AStatusMode.Set,
                targetPlayer = ship.isPlayerShip
            });
        }
        return false;
    }
}
namespace KBraid.BraidEili;

// Code from Eddie mod
internal static class Extensions
{
    static int recursionLevel = 0;

    public static int GetCurrentCostNoRecursion(this Card card, State s)
    {
        int result = 0;
        recursionLevel++;
        if (recursionLevel < 2)
            result = card.GetCurrentCost(s);
        else
            result = card.GetData(s).cost;
        recursionLevel = 0;
        return result;
    }
    public static void TurnCardToEnergyAttack(State s, Combat c, Card? card, CardAction action, Upgrade upgrade)
    {
        if (card == null)
            return;

        int multiplier;
        if (upgrade == Upgrade.B)
        {
            multiplier = 4;
            c.Queue(new ADestroyCard()
            {
                uuid = card.uuid
            });
        }
        else
        {
            multiplier = 2;
            c.Queue(new AExhaustOtherCard
            {
                uuid = card.uuid
            });
        }
        action.timer = 0.2;

        int cost = card.GetCurrentCostNoRecursion(s);
        c.Queue(new AAttack()
        {
            damage = card.GetDmg(s, multiplier * cost)
        });
    }
    public static int GetRandomNonEmptyPart(State s, Combat c, bool targetPlayer)
    {
        var ship = targetPlayer ? s.ship : c.otherShip;
        var rng = new Rand().Next();
        for (var partIndex = 0; partIndex < ship.parts.Count; partIndex++)
            if (rng < (partIndex + 1) / (ship.parts.Count) && (ship.parts[partIndex].type != PType.empty))
                return partIndex;
        return 0;
    }

}
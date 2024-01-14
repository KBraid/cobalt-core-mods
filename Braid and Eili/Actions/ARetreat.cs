namespace KBraid.BraidEili.Actions;

internal class ARetreat : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        c.noReward = true;
        c.PlayerWon(g);
    }
}

namespace KBraid.BraidEili.Actions;

internal class ASacrifice : CardAction
{
    public Upgrade upgrade;

    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        Extensions.TurnCardToEnergyAttack(s, c, card, this, upgrade);
    }
}
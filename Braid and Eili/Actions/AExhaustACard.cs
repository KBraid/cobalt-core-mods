using FSPRO;

namespace KBraid.BraidEili.Actions;

internal class AExhaustACard : CardAction
{
    public int uuid;

    public const double delayTimer = 0.3;

    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;
        Card? card = s.FindCard(uuid);
        if (card != null)
        {
            card.ExhaustFX();
            Audio.Play(Event.CardHandling);
            s.RemoveCardFromWhereverItIs(uuid);
            c.SendCardToExhaust(s, card);
            timer = 0.3;
        }
    }
}

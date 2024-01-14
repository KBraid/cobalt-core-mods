using FSPRO;

namespace KBraid.BraidEili.Actions;

internal class ASacrifice : CardAction
{
    public bool destroy;
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        if (card != null)
        {
            card.temporaryOverride = true;
            c.SendCardToExhaust(s, card);
            if (destroy)
                c.exhausted.Remove(card);
        }
        else
        {
            Audio.Play(Event.CardHandling);
        }
    }
}
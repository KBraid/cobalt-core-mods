using FSPRO;

namespace KBraid.BraidEili.Actions;

internal class AInspiration : CardAction
{
    public bool permanent;
    public Upgrade upgrade;
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = selectedCard;
        if (card != null)
        {
            card.exhaustOverride = false;
            if (permanent)
            {
                card.exhaustOverrideIsPermanent = true;
            }
        }
        else
        {
            Audio.Play(Event.CardHandling);
        }
    }

    public override string? GetCardSelectText(State s)
    {
        return ModEntry.Instance.Localizations.Localize(["card", "Inspiration", "special", upgrade.ToString()]);
    }
}

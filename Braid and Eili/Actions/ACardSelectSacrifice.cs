using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;

internal class ACardSelectSacrifice : ACardSelect
{
    public Upgrade upgrade;
    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> list = new List<Tooltip>();
        if (upgrade != Upgrade.B)
        {
            list.Add(new TTGlossary("cardtrait.exhaust"));
        }
        else
        {
            list.Add(new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.cardtrait,
                () => GetIcon(),
                () => ModEntry.Instance.Localizations.Localize(["cardtrait", "Sacrifice", "name"]),
                () => ModEntry.Instance.Localizations.Localize(["cardtrait", "Sacrifice", "description"]))
                );
        }
        list.Add(new TTGlossary("action.attack.name"));
        return list;
    }
    public static Spr? GetIcon()
    {
        return ModEntry.Instance.ASacrificePermanent.Sprite;
    }
}

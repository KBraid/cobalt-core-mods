using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class BraidSacrifice : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Sacrifice", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Sacrifice", "name"]).Localize
        });
    }
    public override string Name() => "Sacrifice";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 4 : 2;
        data.exhaust = true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        data.description = ModEntry.Instance.Localizations.Localize(["card", "Sacrifice", "description", upgrade.ToString()]);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new()
        {
            new ACardSelectCopy()
            {
                browseAction = new ASacrifice()
                {
                    upgrade = upgrade
                },
                browseSource = upgrade == Upgrade.A ? CardBrowse.Source.Deck : CardBrowse.Source.Hand,
                filterUnremovableAtShops = true,
                upgrade = upgrade
            }
        };
        return actions;
    }
}

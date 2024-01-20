using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidBusterCannon : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BusterCannon", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BusterCannon", "name"]).Localize
        });
    }
    public override string Name() => "Buster Cannon";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 1,
            exhaust = true,
            description = ModEntry.Instance.Localizations.Localize(["card", "BusterCannon", "description", upgrade.ToString()]),
            art = new Spr?(StableSpr.cards_Scattershot)
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.BusterCharge.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterCharge(),
                        amount = 2,
                        destination = CardDestination.Deck,
                        showCardTraitTooltips = false,
                        omitFromTooltips = true,
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterShot(),
                        amount = 1,
                        destination = CardDestination.Deck,
                        showCardTraitTooltips = false
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.BusterCharge.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterCharge()
                        {
                            upgrade = Upgrade.A
                        },
                        amount = 3,
                        destination = CardDestination.Deck,
                        showCardTraitTooltips = false
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterShot(),
                        amount = 1,
                        destination = CardDestination.Deck,
                        showCardTraitTooltips = false,
                        omitFromTooltips = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.BusterCharge.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterCharge()
                        {
                            upgrade = Upgrade.B,
                        },
                        amount = 1,
                        destination = CardDestination.Hand,
                        showCardTraitTooltips = false
                    },
                    new AAddCard()
                    {
                        card = new BraidBusterShot(),
                        amount = 1,
                        destination = CardDestination.Deck,
                        showCardTraitTooltips = false,
                        omitFromTooltips = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}

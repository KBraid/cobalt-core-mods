using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidBusterCharge : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BusterCharge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BusterCharge", "name"]).Localize
        });
    }
    public override string Name() => "Buster Charge";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = upgrade == Upgrade.B ? 1 : 0,
            temporary = true,
            infinite = upgrade == Upgrade.B ? true : false,
            retain = upgrade == Upgrade.B ? true : false,
            art = new Spr?(StableSpr.cards_Scattershot),
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
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ADrawCard()
                    {
                        count = 1
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
                    new ADrawCard()
                    {
                        count = 1
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
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}

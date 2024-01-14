using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;

namespace KBraid.BraidEili.Cards;
public class BraidSacrifice : Card, IModdedCard
{
    public int myDamage;
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
    public void GetSacrificeDmg(State s, Card card)
    {
    }
    public override string Name() => "Sacrifice";
    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = upgrade == Upgrade.B ? 4 : 2;
        data.exhaust = true;
        data.art = new Spr?(StableSpr.cards_Scattershot);
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        /*myDamage = new ACardSelect()
        {
            browseAction = new ASacrifice()
            {
                destroy = false
            },
            browseSource = CardBrowse.Source.Hand
        }.selectedCard.GetCurrentCost(s);*/
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ACardSelect
                    {
                        browseAction = new ASacrifice()
                        {
                            destroy = false
                        },
                        browseSource = CardBrowse.Source.Hand,
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new ACardSelect
                    {
                        browseAction = new ASacrifice()
                        {
                            destroy = true
                        },
                        browseSource = CardBrowse.Source.Hand,
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}

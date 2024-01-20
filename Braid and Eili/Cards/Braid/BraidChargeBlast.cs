using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidChargeBlast : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ChargeBlast", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ChargeBlast", "name"]).Localize
        });
    }
    public override string Name() => "Charge Blast";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 2,
            art = new Spr?(StableSpr.cards_Scattershot),
            description = ModEntry.Instance.Localizations.Localize(["card", "ChargeBlast", "description", upgrade.ToString()], new { Damage = GetDmg(state, GetCardsInHand(state))})
        };
        return data;
    }
    public int GetCardsInHand(State s) => s.route is Combat route ? route.hand.Count - 1 : 0;
    public override List<CardAction> GetActions(State s, Combat c)
    {
        var maxBlastCard = new BraidMaxBlast()
        {
            myDamage = GetCardsInHand(s),
            upgrade = this.upgrade,
        };
        List<CardAction> actions = new()
        {
            new ADiscard(),
            new AAddCard()
            {
                card = maxBlastCard,
                amount = 1,
                destination = CardDestination.Hand
            },
            new AStatus()
            {
                status = Status.energyLessNextTurn,
                statusAmount = 1,
                targetPlayer = true
            }
        };
        return actions;
    }
}

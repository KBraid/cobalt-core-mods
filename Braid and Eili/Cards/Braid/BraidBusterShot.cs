using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidBusterShot : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("BusterShot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                dontOffer = true,
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "BusterShot", "name"]).Localize
        });
    }
    public override string Name() => "Buster Shot";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 1,
            temporary = true,
            art = new Spr?(StableSpr.cards_Scattershot)
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new()
        {
            new AVariableHint()
            {
                status = ModEntry.Instance.BusterCharge.Status
            },
            new AAttack()
            {
                damage = GetDmg(s, s.ship.Get(ModEntry.Instance.BusterCharge.Status)),
                xHint = 1
            },
            new AStatus()
            {
                status = ModEntry.Instance.BusterCharge.Status,
                statusAmount = 0,
                targetPlayer = true,
                mode = AStatusMode.Set,
                omitFromTooltips = true
            },
            new AStatus()
            {
                status = Status.energyLessNextTurn,
                statusAmount = 2,
                targetPlayer = true
            }
        };
        return actions;
    }
}

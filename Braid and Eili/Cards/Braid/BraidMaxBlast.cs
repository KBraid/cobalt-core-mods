using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidMaxBlast : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("MaxBlast", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true,
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "MaxBlast", "name"]).Localize
        });
    }
    public override string Name() => "Max Blast";
    public int myDamage;
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 0,
            exhaust = true,
            temporary = true,
            art = new Spr?(StableSpr.cards_Scattershot),
            description = ModEntry.Instance.Localizations.Localize(["card", "MaxBlast", "description", upgrade.ToString()], new { Damage = GetDmg(state, myDamage) })
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        Upgrade upgrade = this.upgrade;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage)
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage),
                        piercing = true
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s,myDamage),
                        stunEnemy = true
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}

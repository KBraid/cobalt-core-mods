using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class BraidFollowthrough : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Followthrough", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Followthrough", "name"]).Localize
        });
    }
    public override string Name() => "Followthrough";

    public override CardData GetData(State state)
    {
        return new()
        {
            cost = 2,
            floppable = upgrade == Upgrade.B ? false : true,
            flippable = upgrade == Upgrade.B ? true : false,
            exhaust = upgrade == Upgrade.B ? true : false,
            art = flipped ? new Spr?(StableSpr.cards_MiningDrill_Bottom) : new Spr?(StableSpr.cards_MiningDrill_Top),
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.evade,
                        disabled = flipped
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.Get(Status.evade)),
                        xHint = 1,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !flipped
                    }
                    
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.evade,
                        disabled = flipped
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.Get(Status.evade)),
                        xHint = 1,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !flipped
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AVariableHint()
                    {
                        status = Status.evade,
                        //disabled = flipped
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, s.ship.Get(Status.evade)),
                        xHint = 1,
                        //disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 0,
                        mode = AStatusMode.Set,
                        targetPlayer = true,
                        //disabled = flipped
                    },
                    new AMove()
                    {
                        dir = s.ship.Get(Status.evade),
                        xHint = 1,
                        targetPlayer = true,
                        //disabled = flipped
                    },
                    /*new AStatus()
                    {
                        status = Status.evade,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !flipped
                    }*/
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}

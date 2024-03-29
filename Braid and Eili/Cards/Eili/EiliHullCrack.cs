using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliHullCrack : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("HullCrack", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "HullCrack", "name"]).Localize
        });
    }
    public override string Name() => "Hull Crack";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 2,
            exhaust = upgrade == Upgrade.A ? false : true,
            art = ModEntry.Instance.BasicBackground.Sprite,
            //description = ModEntry.Instance.Localizations.Localize(["card", "HullCrack", "description", upgrade.ToString()]),
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
                    new ADummyAction(),
                    new ATooltipDummy()
                    {
                        icons = new()
                        {
                            new Icon(StableSpr.icons_attack, GetDmg(s, 1), Colors.redd),
                            new Icon(ModEntry.Instance.AApplyTempBrittle_Icon.Sprite, null, Colors.textMain)
                        }
                    },
                    new ATempBrittleAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ADummyAction(),
                    new ATooltipDummy()
                    {
                        icons = new()
                        {
                            new Icon(StableSpr.icons_attack, GetDmg(s, 1), Colors.redd),
                            new Icon(ModEntry.Instance.AApplyTempBrittle_Icon.Sprite, null, Colors.textMain)
                        },

                    },
                    new ATempBrittleAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                    },
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        piercing = true,
                        weaken = true,
                    },
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
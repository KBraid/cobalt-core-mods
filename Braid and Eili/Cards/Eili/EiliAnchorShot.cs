using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliAnchorShot : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("AnchorShot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "AnchorShot", "name"]).Localize
        });
    }
    public override string Name() => "Anchor Shot";

    public override CardData GetData(State state)
    {
        CardData data = new CardData();
        data.cost = 1;
        data.exhaust = upgrade == Upgrade.A ? false : true;
        data.art = ModEntry.Instance.BasicBackground.Sprite;
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
                    new AAttack()
                    {
                        damage = 3,
                        status = Status.lockdown,
                        statusAmount = 1,
                        dialogueSelector = ".card_anchorshot_played"
                    },
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 3,
                        status = Status.lockdown,
                        statusAmount = 1,
                        dialogueSelector = ".card_anchorshot_played"
                    },
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = 3,
                        stunEnemy = true,
                        status = Status.lockdown,
                        statusAmount = 1,
                        dialogueSelector = ".card_anchorshot_played"
                    },
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
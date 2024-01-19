using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;
using System.Linq;

namespace KBraid.BraidEili.Cards;
public class BraidShoveIt : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ShoveIt", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ShoveIt", "name"]).Localize
        });
    }
    public override string Name() => "Shove It";

    public override CardData GetData(State state)
    {
        bool flag = false;
        int dmg = 0;
        int num = 0;
        switch (upgrade)
        {
            case Upgrade.None:
                flag = false;
                dmg = 1;
                num = 3;
                break;
            case Upgrade.A:
                flag = false;
                dmg = 2;
                num = 5;
                break;
            case Upgrade.B:
                flag = true;
                dmg = 1;
                num = 3;
                break;
        }
        CardData data = new CardData()
        {
            cost = 1,
            art = new Spr?(StableSpr.cards_Strafe),
            flippable = flag,
            description = upgrade == Upgrade.B ? null : ModEntry.Instance.Localizations.Localize(["card", "ShoveIt", "description"], new { Damage = GetDmg(state, dmg), Move = num })
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
                    new AAttackRandomMove()
                    {
                        damage = GetDmg(s, 1),
                        randomDir = 3
                    }
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new AAttackRandomMove()
                    {
                        damage = GetDmg(s, 2),
                        randomDir = 5
                    }
                };
                actions = cardActionList2;
                break;
            case Upgrade.B:
                List<CardAction> cardActionList3 = new List<CardAction>()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        moveEnemy = 3
                    }
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
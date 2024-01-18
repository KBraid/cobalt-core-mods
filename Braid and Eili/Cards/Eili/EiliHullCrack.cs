using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Linq;
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
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnEnemyGetHit), (State s, Combat c) =>
        {
            var ship = c.otherShip;
            foreach (var part in ((IEnumerable<Part>)ship.parts).Reverse())
            {
                if (!ModEntry.Instance.KokoroApi.TryGetExtensionData(part, "DamageModifierBeforeTempBrittle", out PDamMod damageModifierBeforeTempBrittle))
                    continue;
                ModEntry.Instance.KokoroApi.RemoveExtensionData(part, "DamageModifierBeforeTempBrittle");
                if (part.damageModifier == PDamMod.brittle)
                    part.damageModifier = damageModifierBeforeTempBrittle;
            }
        }, 0);
    }
    public override string Name() => "Hull Crack";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = 2,
            exhaust = upgrade == Upgrade.A ? false : true,
            art = ModEntry.Instance.BasicBackground.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "HullCrack", "description", upgrade.ToString()]),
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
                    new ATempBrittleAttack()
                    {
                        damage = 1,
                        piercing = true,
                    },
                };
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>()
                {
                    new ATempBrittleAttack()
                    {
                        damage = 1,
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
                        damage = 1,
                        piercing = true,
                        brittle = true,
                    },
                };
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
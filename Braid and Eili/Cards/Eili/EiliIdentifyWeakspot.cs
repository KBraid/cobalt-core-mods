using Nickel;
using System.Collections.Generic;
using System.Reflection;
using KBraid.BraidEili.Actions;
using System.Linq;

namespace KBraid.BraidEili.Cards;
public class EiliIdentifyWeakspot : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("IdentifyWeakspot", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "IdentifyWeakspot", "name"]).Localize
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
    public override string Name() => "Identify Weakspot";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = upgrade == Upgrade.A ? 0 : 1,
            exhaust = upgrade == Upgrade.B ? false : true,
            art = ModEntry.Instance.BasicBackground.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "IdentifyWeakspot", "description"]),
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var ship = c.otherShip;
        List<CardAction> actions = new()
        {
            new ATempBrittlePart()
            {
                TargetPlayer = ship.isPlayerShip,
                WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, false)
            }
        };
        return actions;
    }
}
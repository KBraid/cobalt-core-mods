using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
public class EiliExtraPlating : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("ExtraPlating", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.EiliDeck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "ExtraPlating", "name"]).Localize
        });
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatEnd), (State s) =>
        {
            var ship = s.ship;
            foreach (var part in ((IEnumerable<Part>)ship.parts).Reverse())
            {
                if (!ModEntry.Instance.KokoroApi.TryGetExtensionData(part, "DamageModifierBeforeExtraPlatingCombat", out PDamMod damageModifierBeforeExtraPlatingCombat))
                    continue;
                ModEntry.Instance.KokoroApi.RemoveExtensionData(part, "DamageModifierBeforeExtraPlatingCombat");
                if (part.damageModifier == PDamMod.armor && damageModifierBeforeExtraPlatingCombat != PDamMod.armor)
                    part.damageModifier = damageModifierBeforeExtraPlatingCombat;
            }
        }, 0);
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnTurnStart), (State s, Combat c) =>
        {
            if (!c.isPlayerTurn)
                return;
            var ship = s.ship;
            foreach (var part in ((IEnumerable<Part>)ship.parts).Reverse())
            {
                if (!ModEntry.Instance.KokoroApi.TryGetExtensionData(part, "DamageModifierBeforeExtraPlatingTemp", out PDamMod damageModifierBeforeExtraPlatingTemp))
                    continue;
                ModEntry.Instance.KokoroApi.RemoveExtensionData(part, "DamageModifierBeforeExtraPlatingTemp");
                if (part.damageModifier == PDamMod.armor)
                    part.damageModifier = damageModifierBeforeExtraPlatingTemp;
            }
        }, 0);
    }
    public override string Name() => "Extra Plating";

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            cost = upgrade == Upgrade.None ? 3 : 2,
            exhaust = true,
            art = ModEntry.Instance.BasicBackground.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "ExtraPlating", "description", upgrade.ToString()]),
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var ship = s.ship;
        List<CardAction> actions = new();
        switch (upgrade)
        {
            case Upgrade.None:
                List<CardAction> cardActionList1 = new List<CardAction>();
                cardActionList1.Add(new ATempArmorPart
                {
                    TargetPlayer = ship.isPlayerShip,
                    WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, true),
                    onlyOneTurn = false,
                    omitFromTooltips = true,
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add(new ATempArmorPart
                {
                    TargetPlayer = ship.isPlayerShip,
                    WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, true),
                    onlyOneTurn = false,
                    omitFromTooltips = true,
                });
                actions = cardActionList2;
                break;
            case Upgrade.B:
                bool triggeredOnce = false;
                List<CardAction> cardActionList3 = new List<CardAction>();
                for (var partIndex = 0; partIndex < ship.parts.Count; partIndex++)
                    if (ship.parts[partIndex].type != PType.empty)
                    {
                        cardActionList3.Add(new ATempArmorPart
                        {
                            TargetPlayer = ship.isPlayerShip,
                            WorldX = ship.x + partIndex,
                            onlyOneTurn = true,
                            omitFromTooltips = triggeredOnce
                        });
                        triggeredOnce = true;
                    }
                actions = cardActionList3;
                break;
        }
        return actions;
    }
}
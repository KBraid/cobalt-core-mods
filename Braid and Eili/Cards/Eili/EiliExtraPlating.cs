using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
internal static class TempArmorExt
{
    public static PDamMod? GetDamageModifierBeforeTempArmor(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierBeforeTempArmor");

    public static void SetDamageModifierBeforeTempArmor(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierBeforeTempArmor", value);

    public static PDamMod? GetDamageModifierOverrideWhileActiveBeforeTempArmor(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierOverrideWhileActiveBeforeTempArmor");

    public static void SetDamageModifierOverrideWhileActiveBeforeTempArmor(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierOverrideWhileActiveBeforeTempArmor", value);
    public static PDamMod? GetDamageModifierBeforeFullCombatArmor(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierBeforeFullCombatArmor");

    public static void SetDamageModifierBeforeFullCombatArmor(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierBeforeFullCombatArmor", value);

    public static PDamMod? GetDamageModifierOverrideWhileActiveBeforeFullCombatArmor(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierOverrideWhileActiveBeforeFullCombatArmor");

    public static void SetDamageModifierOverrideWhileActiveBeforeFullCombatArmor(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierOverrideWhileActiveBeforeFullCombatArmor", value);
}

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
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnTurnStart), (State state, Combat combat) =>
        {
            if (!combat.isPlayerTurn)
                return;

            List<Ship> ships = [state.ship, combat.otherShip];
            foreach (var ship in ships)
            {
                foreach (var part in ship.parts)
                {
                    if (part.damageModifier == PDamMod.armor && part.GetDamageModifierBeforeTempArmor() is { } damageModifierBeforeTempArmor)
                    {
                        part.damageModifier = damageModifierBeforeTempArmor;
                        part.SetDamageModifierBeforeTempArmor(null);
                    }
                    if (part.damageModifierOverrideWhileActive == PDamMod.armor && part.GetDamageModifierOverrideWhileActiveBeforeTempArmor() is { } damageModifierOverrideWhileActiveBeforeTempArmor)
                    {
                        part.damageModifierOverrideWhileActive = damageModifierOverrideWhileActiveBeforeTempArmor;
                        part.SetDamageModifierOverrideWhileActiveBeforeTempArmor(null);
                    }
                }
            }
        }, 0);
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatEnd), (State state) =>
        {
            var ship = state.ship;
            foreach (var part in ship.parts)
            {
                if (part.damageModifier == PDamMod.armor && part.GetDamageModifierBeforeFullCombatArmor() is { } damageModifierBeforeFullCombatArmor)
                {
                    part.damageModifier = damageModifierBeforeFullCombatArmor;
                    part.SetDamageModifierBeforeFullCombatArmor(null);
                }
                if (part.damageModifierOverrideWhileActive == PDamMod.armor && part.GetDamageModifierOverrideWhileActiveBeforeFullCombatArmor() is { } damageModifierOverrideWhileActiveBeforeFullCombatArmor)
                {
                    part.damageModifierOverrideWhileActive = damageModifierOverrideWhileActiveBeforeFullCombatArmor;
                    part.SetDamageModifierOverrideWhileActiveBeforeFullCombatArmor(null);
                }
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
                    WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, true, "notArmor"),
                    onlyOneTurn = false,
                });
                actions = cardActionList1;
                break;
            case Upgrade.A:
                List<CardAction> cardActionList2 = new List<CardAction>();
                cardActionList2.Add(new ATempArmorPart
                {
                    TargetPlayer = ship.isPlayerShip,
                    WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, true, "notArmor"),
                    onlyOneTurn = false,
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
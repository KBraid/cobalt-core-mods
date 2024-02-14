using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
internal static class TempBrittleExt
{
    public static PDamMod? GetDamageModifierBeforeTempBrittle(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierBeforeTempBrittle");

    public static void SetDamageModifierBeforeTempBrittle(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierBeforeTempBrittle", value);

    public static PDamMod? GetDamageModifierOverrideWhileActiveBeforeTempBrittle(this Part self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<PDamMod>(self, "DamageModifierOverrideWhileActiveBeforeTempBrittle");

    public static void SetDamageModifierOverrideWhileActiveBeforeTempBrittle(this Part self, PDamMod? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "DamageModifierOverrideWhileActiveBeforeTempBrittle", value);
}

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
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnEnemyGetHit), (State state, Combat combat, Part? part) =>
        {
            var ship = combat.otherShip;
            foreach (var spart in ship.parts)
            {
                if (part != null && part == spart)
                {
                    if (spart.damageModifier == PDamMod.brittle && spart.GetDamageModifierBeforeTempBrittle() is { } damageModifierBeforeIdentifyWeakspot)
                    {
                        spart.damageModifier = damageModifierBeforeIdentifyWeakspot;
                        spart.SetDamageModifierBeforeTempBrittle(null);
                    }
                    if (spart.damageModifierOverrideWhileActive == PDamMod.brittle && spart.GetDamageModifierOverrideWhileActiveBeforeTempBrittle() is { } damageModifierOverrideWhileActiveBeforeIdentifyWeakspot)
                    {
                        spart.damageModifierOverrideWhileActive = damageModifierOverrideWhileActiveBeforeIdentifyWeakspot;
                        spart.SetDamageModifierOverrideWhileActiveBeforeTempBrittle(null);
                    }
                }
            }
        }, 0);
    }
    public override string Name() => "Identify Weakspot";

    public override CardData GetData(State state)
    {
        int num = 1;
        switch (upgrade)
        {
            case Upgrade.A:
                num = 0;
                break;
            case Upgrade.B:
                num = 2;
                break;
        }
        return new()
        {
            cost = num,
            exhaust = upgrade == Upgrade.B ? false : true,
            art = ModEntry.Instance.BasicBackground.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "IdentifyWeakspot", "description"]),
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var ship = c.otherShip;
        List<CardAction> actions = new()
        {
            new ATempBrittlePart()
            {
                TargetPlayer = ship.isPlayerShip,
                WorldX = ship.x + Extensions.GetRandomNonEmptyPart(s, c, false, "notBrittle"),
                dialogueSelector = ".card_identifyweakspot_played"
            }
        };
        return actions;
    }
}
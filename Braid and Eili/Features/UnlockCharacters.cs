using HarmonyLib;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;

internal sealed class UnlockCharactersManager
{
    public UnlockCharactersManager()
    {
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(StoryVars), nameof(StoryVars.RecordRunWin)),
            postfix: new HarmonyMethod(GetType(), nameof(StoryVars_RecordRunWin_Postfix))
        );
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(StoryVars), nameof(StoryVars.GetUnlockedChars)),
            postfix: new HarmonyMethod(GetType(), nameof(StoryVars_GetUnlockedChars_Postfix))
        );
        ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += EiliLockedLocale;
        ModEntry.Instance.Helper.Events.OnLoadStringsForLocale += BraidLockedLocale;
    }

    private void EiliLockedLocale(object? sender, Nickel.LoadStringsForLocaleEventArgs e)
    {
        e.Localizations[$"char.{ModEntry.Instance.EiliDeck.Deck}.desc.locked"] = ModEntry.Instance.AnyLocalizations.Bind(["character", "Eili", "locked"]).Localize(e.Locale) ?? "???";
    }
    private void BraidLockedLocale(object? sender, Nickel.LoadStringsForLocaleEventArgs e)
    {
        e.Localizations[$"char.{ModEntry.Instance.BraidDeck.Deck}.desc.locked"] = ModEntry.Instance.AnyLocalizations.Bind(["character", "Braid", "locked"]).Localize(e.Locale) ?? "???";
    }

    private static void StoryVars_RecordRunWin_Postfix(
        StoryVars __instance,
        State state)
    {
        if (FeatureFlags.BypassUnlocks)
            return;
        if (!ModEntry.Instance.LockedChar)
            return;
        if (__instance.winCount > 4)
            __instance.UnlockChar(ModEntry.Instance.EiliDeck.Deck);
        if (state.characters.Any((Character ch) => ch.deckType == ModEntry.Instance.EiliDeck.Deck))
            __instance.UnlockChar(ModEntry.Instance.BraidDeck.Deck);
    }
    private static void StoryVars_GetUnlockedChars_Postfix(
        ref HashSet<Deck> __result)
    {
        if (FeatureFlags.BypassUnlocks)
        {
            __result.Add(ModEntry.Instance.EiliDeck.Deck);
            __result.Add(ModEntry.Instance.BraidDeck.Deck);
        }
    }
}

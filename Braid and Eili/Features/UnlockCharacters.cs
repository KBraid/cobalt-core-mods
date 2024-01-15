using HarmonyLib;
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
            prefix: new HarmonyMethod(GetType(), nameof(StoryVars_RecordRunWin_Prefix))
        );
        ModEntry.Instance.Harmony.Patch(
            original: AccessTools.DeclaredMethod(typeof(StoryVars), nameof(StoryVars.GetUnlockedChars)),
            prefix: new HarmonyMethod(GetType(), nameof(StoryVars_GetUnlockedChars_Prefix))
        );
    }
    private static void StoryVars_RecordRunWin_Prefix(
        StoryVars __instance, 
        State state)
    {
        if (FeatureFlags.BypassUnlocks)
            return;
        if (__instance.winCount > 4)
            __instance.UnlockChar(ModEntry.Instance.EiliDeck.Deck);
        if (state.characters.Any<Character>((Func<Character, bool>)(ch =>
        {
            Deck? deckType = ch.deckType;
            Deck deck = ModEntry.Instance.EiliDeck.Deck;
            return deckType.GetValueOrDefault() == deck & deckType.HasValue;
        })))
            __instance.UnlockChar(ModEntry.Instance.BraidDeck.Deck);
    }
    private static void StoryVars_GetUnlockedChars_Prefix(
        StoryVars __instance)
    {
        if (FeatureFlags.BypassUnlocks)
        {
            __instance.unlockedChars.Add(ModEntry.Instance.EiliDeck.Deck);
            __instance.unlockedChars.Add(ModEntry.Instance.BraidDeck.Deck);
        }
    }
}

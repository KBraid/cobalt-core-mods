using System;

namespace KBraid.BraidEili;

internal sealed class DialogueManager
{
    public DialogueManager()
    {
        CombatDialogue.Inject();
        EventDialogue.Inject();

        foreach (var cardType in ModEntry.AllCards)
        {
            if (Activator.CreateInstance(cardType) is not IModdedCard card)
                continue;
            card.InjectDialogue();
        }
    }

}
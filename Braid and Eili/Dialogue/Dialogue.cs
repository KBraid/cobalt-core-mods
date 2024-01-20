
using System;

namespace KBraid.BraidEili;

internal sealed class Dialogue
{
    public Dialogue()
    {
        CombatDialogue.Inject();

        foreach (var cardType in ModEntry.AllCards)
        {
            if (Activator.CreateInstance(cardType) is not IModdedCard card)
                continue;
            card.InjectDialogue();
        }
    }

}
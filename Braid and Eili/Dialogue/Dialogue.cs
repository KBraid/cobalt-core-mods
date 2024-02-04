using System;

namespace KBraid.BraidEili;

internal sealed class DialogueManager
{
    public DialogueManager()
    {
        CombatDialogue.Inject();
        EventDialogue.Inject();
    }
}
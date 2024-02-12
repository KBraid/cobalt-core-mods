namespace KBraid.BraidEili;

internal sealed class CustomTitle : TitleCard
{
    public string? Text;

    public override bool Execute(G g, IScriptTarget target, ScriptCtx ctx)
    {
        if (target is Dialogue dialogue)
        {
            dialogue.titleCard = ((empty == true) ? null : Text);
        }

        return true;
    }
}
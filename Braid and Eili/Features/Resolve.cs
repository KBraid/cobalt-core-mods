namespace KBraid.BraidEili;
internal sealed class ResolveManager : IStatusLogicHook
{
    public ResolveManager()
    {
        ModEntry.Instance.KokoroApi.RegisterStatusLogicHook(this, 0);
    }
}
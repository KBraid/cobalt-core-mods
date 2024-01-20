using Nickel;

namespace KBraid.BraidEili;

public sealed class ApiImplementation : IBraidEiliApi
{
    public IDeckEntry BraidDeck
        => ModEntry.Instance.BraidDeck;
    public IDeckEntry EiliDeck
        => ModEntry.Instance.EiliDeck;
    public IStatusEntry DisabledDampeners
        => ModEntry.Instance.DisabledDampeners;
    public IStatusEntry ShockAbsorber
        => ModEntry.Instance.ShockAbsorber;
    public IStatusEntry TempShieldNextTurn
        => ModEntry.Instance.TempShieldNextTurn;
    public IStatusEntry KineticGenerator
        => ModEntry.Instance.KineticGenerator;
    public IStatusEntry EqualPayback
        => ModEntry.Instance.EqualPayback;
    public IStatusEntry TempPowerdrive
        => ModEntry.Instance.TempPowerdrive;
    public IStatusEntry Bide
        => ModEntry.Instance.Bide;
    public IStatusEntry PerfectTiming
        => ModEntry.Instance.PerfectTiming;
    public IStatusEntry LostHull
        => ModEntry.Instance.LostHull;
    public IStatusEntry Resolve
        => ModEntry.Instance.Resolve;
    public IStatusEntry Retreat
        => ModEntry.Instance.Retreat;
    public IStatusEntry EngineStallNextTurn
        => ModEntry.Instance.EngineStallNextTurn;
}
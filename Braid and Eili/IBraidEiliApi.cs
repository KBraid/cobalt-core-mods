using Nickel;

namespace KBraid.BraidEili;
public interface IBraidEiliApi
{
    IDeckEntry BraidDeck { get; }
    IDeckEntry EiliDeck { get; }
    IStatusEntry DisabledDampeners { get; }
    IStatusEntry ShockAbsorber { get; }
    IStatusEntry TempShieldNextTurn { get; }
    IStatusEntry KineticGenerator { get; }
    IStatusEntry EqualPayback { get; }
    IStatusEntry TempPowerdrive { get; }
    IStatusEntry Bide { get; }
    IStatusEntry PerfectTiming { get; }
    IStatusEntry LostHull { get; }
    IStatusEntry Resolve { get; }
    IStatusEntry Retreat { get; }
    IStatusEntry EngineStallNextTurn { get; }
}
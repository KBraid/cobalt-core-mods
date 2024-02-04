using Nickel;

namespace KBraid.BraidEili;

internal interface IModdedCard
{
    static abstract void Register(IModHelper helper);
}
internal interface IModdedArtifact
{
    static abstract void Register(IModHelper helper);
}
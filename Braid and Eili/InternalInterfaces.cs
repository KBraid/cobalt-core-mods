using Nickel;

namespace KBraid.BraidEili;

internal interface IModdedCard
{
    static abstract void Register(IModHelper helper);

    float TextScaling
        => 1f;
    float ActionRenderingSpacing
        => 1f;
}
internal interface IModdedArtifact
{
    static abstract void Register(IModHelper helper);
}
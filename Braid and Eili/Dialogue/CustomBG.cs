
namespace KBraid.BraidEili;

public class BGBarren : BG
{
    public override void Render(G g, double t, Vec offset)
    {
        Draw.Sprite(ModEntry.Instance.BGBarren.Sprite, 0.0, 0.0);
    }
}
public class BGMemoryButCool : BG
{
    public override void Render(G g, double t, Vec offset)
    {
        Draw.Sprite(StableSpr.bg_memory, 0.0, 0.0);
    }
}

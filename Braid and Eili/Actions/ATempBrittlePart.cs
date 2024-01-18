using Newtonsoft.Json;
using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;

public sealed class ATempBrittlePart : CardAction
{
    [JsonProperty]
    public required bool TargetPlayer;

    [JsonProperty]
    public required int WorldX;

    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
        timer = 0;

        var ship = TargetPlayer ? s.ship : c.otherShip;
        if (ship.GetPartAtWorldX(WorldX) is not { } part || part.damageModifier == PDamMod.brittle)
            return;

        ModEntry.Instance.KokoroApi.SetExtensionData(part, "DamageModifierBeforeTempBrittle", part.damageModifier);
        c.QueueImmediate(new ABrittle
        {
            targetPlayer = TargetPlayer,
            worldX = WorldX
        });
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> list = new List<Tooltip>()
        {
            new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.parttrait,
                () => GetIcon(),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempBrittle", "name"]),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempBrittle", "description"]))
        };
        return list;
    }
    public static Spr? GetIcon()
    {
        return ModEntry.Instance.AApplyTempBrittle_Icon.Sprite;
    }
}
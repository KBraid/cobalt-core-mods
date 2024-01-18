using Newtonsoft.Json;
using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;
public sealed class ATempArmorPart : CardAction
{
    [JsonProperty]
    public required bool TargetPlayer;

    [JsonProperty]
    public required int WorldX;

    [JsonProperty]
    public required bool onlyOneTurn;

    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
        timer = 0;

        var ship = TargetPlayer ? s.ship : c.otherShip;
        if (ship.GetPartAtWorldX(WorldX) is not { } part || part.damageModifier == PDamMod.armor)
            return;
        var damageType = "DamageModifierBeforeExtraPlatingCombat";
        if (onlyOneTurn)
            damageType = "DamageModifierBeforeExtraPlatingTemp";
        ModEntry.Instance.KokoroApi.SetExtensionData(part, damageType, part.damageModifier);
        c.QueueImmediate(new AArmor
        {
            targetPlayer = TargetPlayer,
            worldX = WorldX,
            omitFromTooltips = true,
        });
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        List<Tooltip> list = new List<Tooltip>()
        {
            new CustomTTGlossary(
                CustomTTGlossary.GlossaryType.parttrait,
                () => GetIcon(),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempArmor", "name"]),
                () => ModEntry.Instance.Localizations.Localize(["action", "TempArmor", onlyOneTurn ? "description" : "altDescription"]))
        };
        return list;
    }
    public static Spr? GetIcon()
    {
        return ModEntry.Instance.AApplyTempArmor_Icon.Sprite;
    }
}
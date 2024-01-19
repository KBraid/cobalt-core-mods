using KBraid.BraidEili.Cards;
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
        if (onlyOneTurn)
        {
            timer = 0;
        }
        var ship = TargetPlayer ? s.ship : c.otherShip;
        if (ship.GetPartAtWorldX(WorldX) is not { } part)
            return;

        if (part.damageModifier != PDamMod.armor)
        {
            if (onlyOneTurn)
            {
                part.SetDamageModifierBeforeTempArmor(part.damageModifier);
            }
            else
            {
                part.SetDamageModifierBeforeFullCombatArmor(part.damageModifier);
            }
            c.QueueImmediate(new AArmor
            {
                targetPlayer = TargetPlayer,
                worldX = WorldX
            });
        }
        if (part.damageModifierOverrideWhileActive is not null && part.damageModifierOverrideWhileActive != PDamMod.armor)
        {
            if (onlyOneTurn)
            {
                part.SetDamageModifierOverrideWhileActiveBeforeTempArmor(part.damageModifierOverrideWhileActive);
            }
            else
            {

                part.SetDamageModifierOverrideWhileActiveBeforeFullCombatArmor(part.damageModifierOverrideWhileActive);
            }
            c.QueueImmediate(new AArmor
            {
                targetPlayer = TargetPlayer,
                worldX = WorldX,
                justTheActiveOverride = true
            });
        }
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
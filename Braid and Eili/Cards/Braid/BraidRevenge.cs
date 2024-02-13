using KBraid.BraidEili.Actions;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
internal static class LostHullExt
{
    public static int? GetLostHullThisCombat(this Combat self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<int>(self, "LostHullThisCombat");

    public static void SetLostHullThisCombat(this Combat self, int? value)
        => ModEntry.Instance.Helper.ModData.SetOptionalModData(self, "LostHullThisCombat", value);
}
public class BraidRevenge : Card, IModdedCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Revenge", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.BraidDeck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Revenge", "name"]).Localize
        });
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnPlayerLoseHull), (State state, Combat combat, int amount) =>
        {
            int num = amount;
            if (combat.GetLostHullThisCombat() is not null)
                num += (int)combat.GetLostHullThisCombat()!;
            combat.SetLostHullThisCombat(num);
        }, 0);
    }
    public override string Name() => "Revenge";
    public override CardData GetData(State state)
    {
        int num = 0;
        var str = "";
        if (state.route is Combat combat)
        {
            num = combat.GetLostHullThisCombat() is not null ? (int)combat.GetLostHullThisCombat()! : 0;
            str = string.Format(" (<c=boldPink>{0}</c>)", GetDmg(state, num));
        }
        return new()
        {
            cost = upgrade == Upgrade.B ? 0 : 3,
            exhaust = upgrade == Upgrade.B ? false : true,
            singleUse = upgrade == Upgrade.B ? true : false,
            retain = upgrade == Upgrade.A ? true : false,
            art = new Spr?(StableSpr.cards_Scattershot),
            //description = ModEntry.Instance.Localizations.Localize(["card", "Revenge", "description", upgrade.ToString()], new { Amount = str })
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var num = c.GetLostHullThisCombat() is not null ? (int)c.GetLostHullThisCombat()! : 0;
        return new()
        {
            new AVariableHintLostHull()
            {
                sprite = ModEntry.Instance.LostHull.Sprite,
                name = ModEntry.Instance.Localizations.Localize(["misc", "lostHull", "name"]),
                amount = num
            },
            new AAttack()
            {
                damage = GetDmg(s, num),
                piercing = true,
                brittle = upgrade == Upgrade.B ? true : false,
                xHint = 1
            }
        };
    }
}

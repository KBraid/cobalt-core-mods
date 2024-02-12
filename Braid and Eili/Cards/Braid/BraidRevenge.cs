using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace KBraid.BraidEili.Cards;
internal static class LostHullExt
{
    public static int? GetLostHullThisCombat(this State self)
        => ModEntry.Instance.Helper.ModData.GetOptionalModData<int>(self, "LostHullThisCombat");

    public static void SetLostHullThisCombat(this State self, int? value)
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
            if (state.GetLostHullThisCombat() is not null)
                num += (int)state.GetLostHullThisCombat()!;
            state.SetLostHullThisCombat(num);
        }, 0);
        helper.Events.RegisterBeforeArtifactsHook(nameof(Artifact.OnCombatEnd), (State state) =>
        {
            state.SetLostHullThisCombat(null);
        }, 0);
    }
    public override string Name() => "Revenge";
    public override CardData GetData(State state)
    {
        var num = state.GetLostHullThisCombat() is not null ? (int)state.GetLostHullThisCombat()! : 0;
        var str = "";
        if (state.route is Combat)
            str = string.Format(" (<c=boldPink>{0}</c>)", GetDmg(state, num));
        return new()
        {
            cost = upgrade == Upgrade.B ? 0 : 3,
            exhaust = upgrade == Upgrade.B ? false : true,
            singleUse = upgrade == Upgrade.B ? true : false,
            retain = upgrade == Upgrade.A ? true : false,
            art = new Spr?(StableSpr.cards_Scattershot),
            description = ModEntry.Instance.Localizations.Localize(["card", "Revenge", "description", upgrade.ToString()], new { Amount = str })
        };
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        var num = s.GetLostHullThisCombat() is not null ? (int)s.GetLostHullThisCombat()! : 0;
        return new()
        {
            new AAttack()
            {
                damage = GetDmg(s, num),
                piercing = true,
                brittle = upgrade == Upgrade.B ? true : false
            }
        };
    }
}

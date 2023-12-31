
using System.Collections.Generic;

#nullable enable
[CardMeta(deck = Deck.eili, rarity = Rarity.common, upgradesTo = new Upgrade[] {Upgrade.A, Upgrade.B})]
public class StunBeam : Card
{
  public override string Name() => "Stun Beam";

  public override CardData GetData(State state)
  {
    CardData data = new CardData();
    Upgrade upgrade1 = this.upgrade;
    int num;
    switch (upgrade1)
    {
      case Upgrade.None:
        num = 1;
        break;
      case Upgrade.A:
        num = 2;
        break;
      case Upgrade.B:
        num = 3;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade1);
        break;
    }
    data.cost = num;
    Upgrade upgrade2 = this.upgrade;
    bool flag;
    switch (upgrade2)
    {
      case Upgrade.None:
        flag = false;
        break;
      case Upgrade.A:
        flag = false;
        break;
      case Upgrade.B:
        flag = true;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade2);
        break;
    }
    data.exhaust = flag;
    data.art = new Spr?(Spr.cards_StunCharge);
    return data;
  }

  public override List<CardAction> GetActions(State s, Combat c)
  {
    Upgrade upgrade = this.upgrade;
    List<CardAction> actions;
    switch (upgrade)
    {
      case Upgrade.None:
        List<CardAction> cardActionList1 = new List<CardAction>();
        cardActionList1.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 0),
          piercing = true
          stunEnemy = true
        });
        actions = cardActionList1;
        break;
      case Upgrade.A:
        List<CardAction> cardActionList2 = new List<CardAction>();
        cardActionList2.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 0),
          piercing = true
          stunEnemy = true
          damage = this.GetDmg(s, 0),
        });
        AAttack aattack1 = new AAttack();
        aattack1.damage = this.GetDmg(s, 0);
        aattack1.fast = true;
        aattack1.piercing = true;
        aattack1.stunEnemy = true;
        aattack1.omitFromTooltips = true;
        cardActionList1.Add((CardAction) aattack1);
        actions = cardActionList2;
        break;
      case Upgrade.B:
        List<CardAction> cardActionList3 = new List<CardAction>();
        cardActionList3.Add((CardAction) new AAttack()
        {
          damage = this.GetDmg(s, 1),
          piercing = true
          stunEnemy = true
        });
        actions = cardActionList3;
        break;
      default:
        // ISSUE: reference to a compiler-generated method
        \u003CPrivateImplementationDetails\u003E.ThrowSwitchExpressionException((object) upgrade);
        break;
    }
    return actions;
  }
}
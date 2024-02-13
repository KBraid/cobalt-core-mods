using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;
public class AVariableHintLostHull : AVariableHint
{
    public required Spr sprite;
    public required string name;
    public required int amount;
    public AVariableHintLostHull() : base()
    {
        hand = true;
    }
    public override Icon? GetIcon(State s)
    {
        return new Icon(sprite, null, Colors.textMain);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        return new()
        {
            new TTGlossary("action.xHint.desc",
                "<c=status>" + name.ToUpperInvariant() + "</c>",
                (s.route is Combat) ? $" </c>(<c=keyword>{amount}</c>)" : "",
                "", ""
            )
        };
    }
}
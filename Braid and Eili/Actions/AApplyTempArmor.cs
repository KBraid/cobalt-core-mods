﻿using System.Collections.Generic;

namespace KBraid.BraidEili.Actions;

public class AApplyTempArmor : CardAction
{
    public bool all;
    public bool onlyOneTurn;
    public override void Begin(G g, State s, Combat c)
    {
    }
    public override List<Tooltip> GetTooltips(State s)
    {
        return new List<Tooltip>()
        {
        //    (Tooltip) new TTGlossary(ModEntry.Instance.A?.Head ?? throw new Exception("Missing ACobraField_Glossary"), Array.Empty<object>())
        };
    }

    public override Icon? GetIcon(State s) => new Icon?(new Icon(ModEntry.Instance.AApplyTempArmor_Icon.Sprite, new int?(), Colors.textMain));
}
﻿using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwosCompany.Helper {

    public static class ManifHelper {
        public static String[] cardNames = new string[] {
            // 0-20 Eili
            "Padding",
            "PlanAhead",
            "IdentifyWeakpoint",
            "ShockAbsorption",
            "DisableDampeners",
            "StunBeam",
            "Bap",
            "DumpPower",
            "Hotwire",
            "ExtraPlating",
            "SpeedIsSafety",
            "HullCrack",
            "AnchorShot",
            "Foresight",
            "HackFlightControls",
            "Improvising",
            "PickMeUp",
            "Inspiration",
            "TargettingScramble",
            "ReroutePower",
            "DumpCargo",
            // 21-32: Isabelle
            "BigHit",
            "ShoveIt",
            "Driveby",
            "Pummel",
            "LeftHook",
            "Haymaker",
            "InductionCoils",
            "ChargeBlast",
            "MaxBlast",
            "LimiterOff",
            "SneakAttack",
            "Followthrough",
            "Retaliate",
            "Windup",
            "Bide",
            "ArmourLock",
            "Discharge",
            "Sacrifice",
            "Revenge",
            "MissileBarrage",
            "Resolve",
            "Retreat",
        };

        public static String[] cardLocs = new string[] {
            // 0-20 Eili
            "Padding",
            "Plan Ahead",
            "Identify Weakpoint",
            "Shock Absorption",
            "Disable Dampeners",
            "Stun Beam",
            "Bap",
            "Dump Power",
            "Hotwire",
            "Extra Plating",
            "Speed Is Safety",
            "Hull Crack",
            "Anchor Shot",
            "Foresight",
            "Hack Flight Controls",
            "Improvising",
            "Pick-Me-Up",
            "Inspiration",
            "Targetting Scramble",
            "Reroute Power",
            "Dump Cargo!",
            // 21-32: Isabelle
            "Big Hit",
            "Shove It",
            "Driveby",
            "Pummel",
            "Left Hook",
            "Haymaker",
            "Induction Coils",
            "Charge Blast",
            "Max Blast",
            "Limiter Off",
            "Sneak Attack",
            "Followthrough",
            "Retaliate",
            "Windup",
            "Bide",
            "Armour Lock",
            "Discharge",
            "Sacrifice",
            "Revenge",
            "Missile Barrage",
            "Resolve",
            "Retreat",
        };

        public static string[] defaultArtCards = new string[] {
        };

        public static Dictionary<string, string> cardTexts = new Dictionary<string, string> {
            {"IdentifyWeakpoint", "Add Brittle to random tile on enemy ship. removed on damage."},
            {"IdentifyWeakpointA", "Add Brittle to random tile on enemy ship. removed on damage."},
            {"IdentifyWeakpointB", "Add Brittle to random tile on enemy ship. removed on damage."},
            {"ExtraPlating", "Armored added to random ship part till end of combat."},
            {"ExtraPlatingA", "Armored added to random ship part till end of combat."},
            {"ExtraPlatingB", "Armored added to entire ship till end of turn."},
            {"Inspiration", "Pick a card from your hand and remove its <c=cardtrait>exhaust</c>."},
            {"InspirationA", "Pick a card from your hand and remove its <c=cardtrait>exhaust</c>."},
            {"InspirationB", "Pick a card from your <c=keyword>draw pile</c> and remove its <c=cardtrait>exhaust</c>."},
            {"TargettingScramble", "reverse direction of all <c=keyword>midrow</c> objects."},
            {"TargettingScrambleA", "reverse direction of all <c=keyword>midrow</c> objects."},
            {"TargettingScrambleB", "reverse direction of all <c=keyword>midrow</c> objects."},
            {"DumpCargo", "Both ships immediately launch Asteroids from all missile tubes."},
            {"DumpCargoA", "Both ships immediately launch Mines from all missile tubes."},
            {"DumpCargoB", "Both ships immediately launch Missiles from all missile tubes."},

            {"ChargeBlast", "<c=keyword>discard</c> your hand. add <c=card>Max Blast</c> to your hand. <c=downside>-1 Energy next turn.</c>"},
            {"ChargeBlastA", "<c=keyword>discard</c> your hand. add <c=card>Max Blast A</c> to your hand. <c=downside>-1 Energy next turn.</c>"},
            {"ChargeBlastB", "<c=keyword>discard</c> your hand. add <c=card>Max Blast B</c> to your hand. <c=downside>-1 Energy next turn.</c>"},
            {"Bide", "Damage recieved is stored and added to next <c=keyword>attack</c>."},
            {"BideA", "Damage recieved is stored and added to next <c=keyword>attack</c>."},
            {"BideB", "Damage recieved is stored and added to next <c=keyword>attack</c>."},
            {"Sacrifice", "<c=keyword>Exhaust </c> a card in your hand and deal its cost X2 in damage."},
            {"SacrificeA", "<c=keyword>Exhaust</c> a card in your <c=keyword>draw pile</c> and deal its cost X2 in damage."},
            {"SacrificeB", "<c=downside>DESTROY</c> a card in your hand and deal its cost X3 in damage."},
            {"Retreat", "<c=keyword>End combat</c> next turn and skip all rewards. <c=downside>Cannot be played during boss battles.</c>"},
            {"RetreatA", "<c=keyword>End combat</c> next turn and skip all rewards. <c=downside>Cannot be played during boss battles.</c>"},
            {"RetreatB", "<c=keyword>End combat</c> next turn and skip all rewards. <c=downside>Cannot be played during boss battles.</c>"},
        };

        public static Dictionary<string, string> charStoryNames = new Dictionary<string, string> {
            { "eili", "EiliDeck" },
            { "braid", "BraidDeck" }
        };
        public static int GetDeckId(string deck) {
            if (deck == "eili")
                return (int)Manifest.EiliDeck!.Id!;
            else if (deck == "braid")
                return (int) Manifest.BraidDeck!.Id!;
            return 0;
        }

        public static int numCards() {
            return cardNames.Length;
        }
    }
}

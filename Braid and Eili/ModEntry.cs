using HarmonyLib;
using KBraid.BraidEili.Cards;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using Nickel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;

/* TO-DO
 * DONE : DisabledDampeners Status
 * DONE : ShockAbsorber Status
 * DONE : TempShieldNextTurn Status
 * DONE : KineticGenerator Status
 * DONE : EqualPayback Status
 * DONE : TempPowerdrive Status
 * DONE : Bide Status
 * DONE : Perfect Timing Status
 * DONE : Revenge: Lost Hull Status
 * DONE : Resolve Status
 * KINDA: ARetreat Action
 *        ISSUE: No retreat animation
 * DONE : AInspiration Action
 * DONE : ASacrifice Action + Extensions + ACardSelectSacrifice
 * DONE : ALaunchMidrow Action + AEnemyVolleySpawnFromAllMissileBays Action
 * DONE : Unlock Req:   Eili : Win 5 times
 *                      Braid : Win with Eili
 * DONE : ATempBrittlePart Action + ATempBrittleAttack Action
 * DONE : ATempArmorPart Action
 * DONE : Removed Coils card, added B.Cannon, B.Charge, B.Shot cards & B.Charge status
 * KINDA: Shove It random move
 *        ISSUE: Render move random alongside attack. Currently using text instead
 * REMAINING STUFF TO-DO
 * Eili Artifacts
 * Braid Artifacts
 * Story
 */
public sealed class ModEntry : SimpleMod
{
    public static string Name => "KBraid.BraidEili";
    internal bool LockedChar = false;
    internal static ModEntry Instance { get; private set; } = null!;
    internal Harmony Harmony { get; }
    internal IKokoroApi KokoroApi { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }
    internal ILocalizationProvider<IReadOnlyList<string>> AnyStoryLoc { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> StoryLocs { get; }
    internal IDeckEntry BraidDeck { get; }
    internal IDeckEntry EiliDeck { get; }
    internal ICharacterEntry BraidChar { get; }
    internal ICharacterEntry EiliChar { get; }
    internal static Color BraidColor => new("c0c9e6");
    internal static Color EiliColor => new("42add1");
    internal static Color BraidCardTitleColor => new("000000");
    internal static Color EiliCardTitleColor => new("000000");
    internal ISpriteEntry BasicBackground { get; }
    internal ISpriteEntry ASacrificePermanent { get; }
    internal ISpriteEntry AApplyTempBrittle_Icon { get; }
    internal ISpriteEntry AApplyTempArmor_Icon { get; }
    internal ISpriteEntry ARandomMove { get; }

    //internal ISpriteEntry EiliUncommonBorder { get; }
    //internal ISpriteEntry EiliRareBorder { get; }

    internal IStatusEntry DisabledDampeners { get; }
    internal IStatusEntry ShockAbsorber { get; }
    internal IStatusEntry TempShieldNextTurn { get; }
    internal IStatusEntry KineticGenerator { get; }
    internal IStatusEntry EqualPayback { get; }
    internal IStatusEntry TempPowerdrive { get; }
    internal IStatusEntry Bide { get; }
    internal IStatusEntry PerfectTiming { get; }
    internal IStatusEntry LostHull { get; }
    internal IStatusEntry Resolve { get; }
    internal IStatusEntry Retreat { get; }
    internal IStatusEntry EngineStallNextTurn { get; }
    internal IStatusEntry BusterCharge { get; }
    internal IList<string> FaceSprites { get; } = [
        "blink",
        "crystallized",
        "defeat",
        "ending_a",
        "ending_b",
        "ending_c",
        "eyes_closed",
        "hug_a",
        "hug_b",
        "mini",
        "neutral",
        "serious",
        "serious_a",
        "serious_b",
        "serious_c",
        "shout",
        "shout_a",
        "shout_b",
        "shout_c",
        "unamused",
        "cheeky",
        "cheeky_a",
        "concerned",
        "happy",
        "manic",
        "sad"
    ];
    internal IList<string> CharNames { get; } = [
        "braid",
        "eili"
    ];
    internal Dictionary<string, ISpriteEntry> Sprites { get; } = [];

    internal static IReadOnlyList<Type> EiliStarterCardTypes { get; } = [
        typeof(EiliPadding),
        typeof(EiliPlanAhead),
    ];

    internal static IReadOnlyList<Type> EiliCommonCardTypes { get; } = [
        typeof(EiliIdentifyWeakspot),
        typeof(EiliShockAbsorption),
        typeof(EiliDisableDampeners),
        typeof(EiliStunBeam),
        typeof(EiliBap),
        typeof(EiliDumpPower),
        typeof(EiliHotwire)
    ];

    internal static IReadOnlyList<Type> EiliUncommonCardTypes { get; } = [
        typeof(EiliExtraPlating),
        typeof(EiliSpeedIsSafety),
        typeof(EiliHullCrack),
        typeof(EiliAnchorShot),
        typeof(EiliForesight),
        typeof(EiliHackFlightControls),
        typeof(EiliImprovising)
    ];
    internal static IReadOnlyList<Type> EiliRareCardTypes { get; } = [
        typeof(EiliPickMeUp),
        typeof(EiliInspiration),
        typeof(EiliTargettingScramble),
        typeof(EiliReroutePower),
        typeof(EiliDumpCargo)
    ];

    internal static IReadOnlyList<Type> BraidStarterCardTypes { get; } = [
        typeof(BraidBigHit),
        typeof(BraidLeftHook)
    ];

    internal static IReadOnlyList<Type> BraidCommonCardTypes { get; } = [
        typeof(BraidDriveby),
        typeof(BraidPummel),
        typeof(BraidHaymaker),
        typeof(BraidChargeBlast),
        typeof(BraidMaxBlast),
        typeof(BraidLimiterOff),
        typeof(BraidSneakAttack),
        typeof(BraidShoveIt)
    ];

    internal static IReadOnlyList<Type> BraidUncommonCardTypes { get; } = [
        typeof(BraidArmourLock),
        typeof(BraidBide),
        typeof(BraidBusterCannon),
        typeof(BraidBusterCharge),
        typeof(BraidBusterShot),
        typeof(BraidDischarge),
        typeof(BraidFollowthrough),
        typeof(BraidRetaliate),
        typeof(BraidWindup),
    ];

    internal static IReadOnlyList<Type> BraidRareCardTypes { get; } = [
        typeof(BraidSacrifice),
        typeof(BraidRevenge),
        typeof(BraidMissileBarrage),
        typeof(BraidResolve),
        typeof(BraidRetreat),
    ];
    internal static IReadOnlyList<Type> EiliCommonArtifactTypes { get; } = [

    ];
    internal static IReadOnlyList<Type> EiliBossArtifactTypes { get; } = [

    ];
    internal static IReadOnlyList<Type> BraidCommonArtifactTypes { get; } = [

    ];
    internal static IReadOnlyList<Type> BraidBossArtifactTypes { get; } = [

    ];

    internal static IEnumerable<Type> EiliCardTypes
        => EiliStarterCardTypes
        .Concat(EiliCommonCardTypes)
        .Concat(EiliUncommonCardTypes)
        .Concat(EiliRareCardTypes);
    internal static IEnumerable<Type> BraidCardTypes
        => BraidStarterCardTypes
        .Concat(BraidCommonCardTypes)
        .Concat(BraidUncommonCardTypes)
        .Concat(BraidRareCardTypes);
    internal static IEnumerable<Type> AllCards
        => EiliCardTypes
        .Concat(BraidCardTypes);
    internal static IEnumerable<Type> EiliArtifactTypes
        => EiliCommonArtifactTypes
        .Concat(EiliBossArtifactTypes);
    internal static IEnumerable<Type> BraidArtifactTypes
        => BraidCommonArtifactTypes
        .Concat(BraidBossArtifactTypes);
    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;
        Harmony = new(package.Manifest.UniqueName);
        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!;
        //KokoroApi.RegisterTypeForExtensionData(typeof(AHurt));
        //KokoroApi.RegisterTypeForExtensionData(typeof(AAttack));
        //KokoroApi.RegisterCardRenderHook(new SpacingCardRenderHook(), 0);
        // Make stuff do stuff
        _ = new DisabledDampenersManager();
        _ = new ShockAbsorberManager();
        _ = new TempShieldNextTurnManager();
        _ = new KineticGeneratorManager();
        _ = new TempPowerdriveManager();
        _ = new BideManager();
        _ = new PerfectTimingManager();
        _ = new LostHullManager();
        _ = new ResolveManager();
        _ = new RetreatManager();
        _ = new EngineStallNextTurnManager();
        _ = new ATooltipDummyManager();
        _ = new ShipPatchesManager();

        CustomTTGlossary.ApplyPatches(Harmony);

        this.AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        this.Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyLocalizations)
        );
        this.AnyStoryLoc = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/story/story_{locale}.json").OpenRead()
        );
        this.StoryLocs = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(this.AnyStoryLoc)
        );
        BasicBackground = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cards/empty_backgroud.png"));
        ASacrificePermanent = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/sacrificePermanent.png"));
        AApplyTempBrittle_Icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/tempBrittle.png"));
        AApplyTempArmor_Icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/tempArmorAction.png"));
        ARandomMove = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/randomMove.png"));

        //EiliUncommonBorder = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cardShared/border_eili_uncommon.png"));
        //EiliRareBorder = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/cardShared/border_eili_rare.png"));

        // Register sprites
        foreach (string name in CharNames)
        {
            //talking sprites such as happy and serious
            foreach (string face in FaceSprites)
            {
                IFileInfo file;
                for (int frame = 0; frame < 10; frame++)
                {
                    file = package.PackageRoot.GetRelativeFile($"assets/characters/{name}/{name}_{face}_{frame}.png");
                    if (file.Exists)
                        Sprites.Add(key: $"{name}_{face}_{frame}", value: Helper.Content.Sprites.RegisterSprite(file));
                    else
                        break;
                }
                // Panels
                file = package.PackageRoot.GetRelativeFile($"assets/panels/char_{name}.png");
                if (file.Exists && !Sprites.ContainsKey($"{name}_panel"))
                    Sprites.Add(key: $"{name}_panel", value: Helper.Content.Sprites.RegisterSprite(file));
                // Card Borders
                file = package.PackageRoot.GetRelativeFile($"assets/cardShared/border_{name}.png");
                if (file.Exists && !Sprites.ContainsKey($"{name}_border"))
                    Sprites.Add(key: $"{name}_border", value: Helper.Content.Sprites.RegisterSprite(file));
                file = package.PackageRoot.GetRelativeFile($"assets/fullchars/{name}_end.png");
                // Full bodies
                if (file.Exists && !Sprites.ContainsKey($"{name}_fullchar"))
                    Sprites.Add(key: $"{name}_fullchar", value: Helper.Content.Sprites.RegisterSprite(file));
            }

        }

        // Register decks

        EiliDeck = Helper.Content.Decks.RegisterDeck("Eili", new()
        {
            Definition = new() { color = EiliColor, titleColor = EiliCardTitleColor },
            DefaultCardArt = BasicBackground.Sprite,
            BorderSprite = Sprites["eili_border"].Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Eili", "name"]).Localize,
        });
        BraidDeck = Helper.Content.Decks.RegisterDeck("Braid", new()
        {
            Definition = new() { color = BraidColor, titleColor = BraidCardTitleColor },
            DefaultCardArt = BasicBackground.Sprite,
            BorderSprite = Sprites["braid_border"].Sprite,
            Name = this.AnyLocalizations.Bind(["character", "Braid", "name"]).Localize
        });

        // Register statuses
        DisabledDampeners = Helper.Content.Statuses.RegisterStatus("DisabledDampeners", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/disabledDampeners.png")).Sprite,
                color = new("42add1"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "DisabledDampeners", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "DisabledDampeners", "description"]).Localize
        });
        ShockAbsorber = Helper.Content.Statuses.RegisterStatus("ShockAbsorption", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/shockAbsorber.png")).Sprite,
                color = new("42add1"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "ShockAbsorber", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "ShockAbsorber", "description"]).Localize
        });
        TempShieldNextTurn = Helper.Content.Statuses.RegisterStatus("TempShieldNextTurn", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/tempShieldNextTurn.png")).Sprite,
                color = new("42add1"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "TempShieldNextTurn", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "TempShieldNextTurn", "description"]).Localize
        });
        KineticGenerator = Helper.Content.Statuses.RegisterStatus("KineticGenerator", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/kineticGenerator.png")).Sprite,
                color = new("42add1"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "KineticGenerator", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "KineticGenerator", "description"]).Localize
        });
        EqualPayback = Helper.Content.Statuses.RegisterStatus("EqualPayback", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/equalPayback.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "EqualPayback", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "EqualPayback", "description"]).Localize
        });
        TempPowerdrive = Helper.Content.Statuses.RegisterStatus("TempPowerdrive", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/tempPowerdrive.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "TempPowerdrive", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "TempPowerdrive", "description"]).Localize
        });
        Bide = Helper.Content.Statuses.RegisterStatus("Bide", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/bide.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "Bide", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Bide", "description"]).Localize
        });
        PerfectTiming = Helper.Content.Statuses.RegisterStatus("PerfectTiming", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/perfectTiming.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "PerfectTiming", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "PerfectTiming", "description"]).Localize
        });
        LostHull = Helper.Content.Statuses.RegisterStatus("LostHull", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/lostHull.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true

            },
            Name = this.AnyLocalizations.Bind(["status", "LostHull", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "LostHull", "description"]).Localize
        });
        Resolve = Helper.Content.Statuses.RegisterStatus("Resolve", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/resolve.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "Resolve", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Resolve", "description"]).Localize
        });
        Retreat = Helper.Content.Statuses.RegisterStatus("Retreat", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/retreat.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "Retreat", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "Retreat", "description"]).Localize
        });
        EngineStallNextTurn = Helper.Content.Statuses.RegisterStatus("EngineStallNextTurn", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/engineStallNextTurn.png")).Sprite,
                color = new("c0c9e6"),
                isGood = false
            },
            Name = this.AnyLocalizations.Bind(["status", "EngineStallNextTurn", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "EngineStallNextTurn", "description"]).Localize
        });
        BusterCharge = Helper.Content.Statuses.RegisterStatus("BusterCharge", new()
        {
            Definition = new()
            {
                icon = Helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/icons/busterCharge.png")).Sprite,
                color = new("c0c9e6"),
                isGood = true
            },
            Name = this.AnyLocalizations.Bind(["status", "BusterCharge", "name"]).Localize,
            Description = this.AnyLocalizations.Bind(["status", "BusterCharge", "description"]).Localize
        });

        // Register cards
        foreach (var cardType in BraidCardTypes)
            AccessTools.DeclaredMethod(cardType, nameof(IModdedCard.Register))?.Invoke(null, [helper]);
        foreach (var cardType in EiliCardTypes)
            AccessTools.DeclaredMethod(cardType, nameof(IModdedCard.Register))?.Invoke(null, [helper]);

        // Register characters
        EiliChar = Helper.Content.Characters.RegisterCharacter("Eili", new()
        {
            Deck = EiliDeck.Deck,
            StartLocked = LockedChar,
            Description = this.AnyLocalizations.Bind(["character", "Eili", "description"]).Localize,
            BorderSprite = Sprites["eili_panel"].Sprite,
            StarterCardTypes = EiliStarterCardTypes,
            NeutralAnimation = new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                ]
            },
            MiniAnimation = new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "mini",
                Frames = [
                   Sprites["eili_mini_0"].Sprite
                ]
            }
        });
        BraidChar = Helper.Content.Characters.RegisterCharacter("Braid", new()
        {
            Deck = BraidDeck.Deck,
            StartLocked = LockedChar,
            Description = this.AnyLocalizations.Bind(["character", "Braid", "description"]).Localize,
            BorderSprite = Sprites["braid_panel"].Sprite,
            StarterCardTypes = BraidStarterCardTypes,
            NeutralAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "neutral",
                Frames = [
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                ]
            },
            MiniAnimation = new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "mini",
                Frames = [
                    Sprites["braid_mini_0"].Sprite
                ]
            },
        });

        //Register Extra Animations
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_gameover",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "gameover",
                Frames = [
                    Sprites["eili_defeat_0"].Sprite
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_cheeky",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "cheeky",
                Frames = [
                    Sprites["eili_cheeky_0"].Sprite,
                    Sprites["eili_cheeky_1"].Sprite,
                    Sprites["eili_cheeky_0"].Sprite,
                    Sprites["eili_cheeky_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_cheeky_a",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "cheeky_a",
                Frames = [
                    Sprites["eili_cheeky_a_0"].Sprite,
                    Sprites["eili_cheeky_a_1"].Sprite,
                    Sprites["eili_cheeky_a_0"].Sprite,
                    Sprites["eili_cheeky_a_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_concerned",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "concerned",
                Frames = [
                    Sprites["eili_concerned_0"].Sprite,
                    Sprites["eili_concerned_1"].Sprite,
                    Sprites["eili_concerned_0"].Sprite,
                    Sprites["eili_concerned_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_happy",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "happy",
                Frames = [
                    Sprites["eili_happy_0"].Sprite,
                    Sprites["eili_happy_1"].Sprite,
                    Sprites["eili_happy_0"].Sprite,
                    Sprites["eili_happy_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_manic",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "manic",
                Frames = [
                    Sprites["eili_manic_0"].Sprite,
                    Sprites["eili_manic_1"].Sprite,
                    Sprites["eili_manic_0"].Sprite,
                    Sprites["eili_manic_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_sad",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "sad",
                Frames = [
                    Sprites["eili_sad_0"].Sprite,
                    Sprites["eili_sad_1"].Sprite,
                    Sprites["eili_sad_0"].Sprite,
                    Sprites["eili_sad_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "eili_squint",
            new()
            {
                Deck = EiliDeck.Deck,
                LoopTag = "squint",
                Frames = [
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                    Sprites["eili_neutral_0"].Sprite,
                    Sprites["eili_neutral_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_gameover",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "gameover",
                Frames = [
                    Sprites["braid_defeat_0"].Sprite
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_squint",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "squint",
                Frames = [
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                    Sprites["braid_neutral_0"].Sprite,
                    Sprites["braid_neutral_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_blink",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "blink",
                Frames = [
                    Sprites["braid_blink_0"].Sprite,
                    Sprites["braid_blink_1"].Sprite,
                    Sprites["braid_blink_2"].Sprite,
                    Sprites["braid_blink_3"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_ending_a",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "ending_a",
                Frames = [
                    Sprites["braid_ending_a_0"].Sprite,
                    Sprites["braid_ending_a_1"].Sprite,
                    Sprites["braid_ending_a_2"].Sprite,
                    Sprites["braid_ending_a_3"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_ending_b",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "ending_b",
                Frames = [
                    Sprites["braid_ending_b_0"].Sprite,
                    Sprites["braid_ending_b_1"].Sprite,
                    Sprites["braid_ending_b_0"].Sprite,
                    Sprites["braid_ending_b_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_ending_c",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "ending_c",
                Frames = [
                    Sprites["braid_ending_c_0"].Sprite,
                    Sprites["braid_ending_c_1"].Sprite,
                    Sprites["braid_ending_c_2"].Sprite,
                    Sprites["braid_ending_c_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_eyes_closed",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "eyes_closed",
                Frames = [
                    Sprites["braid_eyes_closed_0"].Sprite,
                    Sprites["braid_eyes_closed_1"].Sprite,
                    Sprites["braid_eyes_closed_0"].Sprite,
                    Sprites["braid_eyes_closed_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_hug_a",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "hug_a",
                Frames = [
                    Sprites["braid_hug_a_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_hug_b",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "hug_b",
                Frames = [
                    Sprites["braid_hug_b_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_serious",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "serious",
                Frames = [
                    Sprites["braid_serious_0"].Sprite,
                    Sprites["braid_serious_1"].Sprite,
                    Sprites["braid_serious_0"].Sprite,
                    Sprites["braid_serious_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_serious_a",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "serious_a",
                Frames = [
                    Sprites["braid_serious_a_0"].Sprite,
                    Sprites["braid_serious_a_1"].Sprite,
                    Sprites["braid_serious_a_0"].Sprite,
                    Sprites["braid_serious_a_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_serious_b",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "serious_b",
                Frames = [
                    Sprites["braid_serious_b_0"].Sprite,
                    Sprites["braid_serious_b_1"].Sprite,
                    Sprites["braid_serious_b_0"].Sprite,
                    Sprites["braid_serious_b_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_serious_c",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "serious_c",
                Frames = [
                    Sprites["braid_serious_c_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_shout",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "shout",
                Frames = [
                    Sprites["braid_shout_0"].Sprite,
                    Sprites["braid_shout_1"].Sprite,
                    Sprites["braid_shout_0"].Sprite,
                    Sprites["braid_shout_1"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_shout_a",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "shout_a",
                Frames = [
                    Sprites["braid_shout_a_0"].Sprite,
                    Sprites["braid_shout_a_1"].Sprite,
                    Sprites["braid_shout_a_2"].Sprite,
                    Sprites["braid_shout_a_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_shout_b",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "shout_b",
                Frames = [
                    Sprites["braid_shout_b_0"].Sprite,
                    Sprites["braid_shout_b_1"].Sprite,
                    Sprites["braid_shout_b_2"].Sprite,
                    Sprites["braid_shout_b_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_shout_c",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "shout_c",
                Frames = [
                    Sprites["braid_shout_c_0"].Sprite,
                    Sprites["braid_shout_c_1"].Sprite,
                    Sprites["braid_shout_c_2"].Sprite,
                    Sprites["braid_shout_c_0"].Sprite,
                ]
            }
        );
        Helper.Content.Characters.RegisterCharacterAnimation(
            "braid_unamused",
            new()
            {
                Deck = BraidDeck.Deck,
                LoopTag = "unamused",
                Frames = [
                    Sprites["braid_unamused_0"].Sprite,
                    Sprites["braid_unamused_1"].Sprite,
                    Sprites["braid_unamused_0"].Sprite,
                    Sprites["braid_unamused_1"].Sprite,
                ]
            }
        );
        // CHARACTER UNLOCK
        _ = new UnlockCharactersManager();
        // TRANSPILER STUFF
        _ = new EqualPaybackManager();

        _ = new Dialogue();
    }
}

using System.Linq;

namespace KBraid.BraidEili;

internal static class CombatDialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string eili = Instance.EiliDeck.UniqueName;
        string braid = Instance.BraidDeck.UniqueName;
        var currentStory = string.Empty;
        var loopTag = string.Empty;

        DB.story.GetNode("CrabFacts1_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = braid,
            Text = "...",

        });
        DB.story.GetNode("CrabFacts2_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = braid,
            Text = "...",

        });
        DB.story.GetNode("CrabFactsAreOverNow_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
        {
            who = braid,
            Text = "...",

        });
        DB.story.all[currentStory = $"{braid}_WeGotHurtButNotTooBad_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                braid
            },
            enemyShotJustHit = true,
            minDamageDealtToPlayerThisTurn = 1,
            maxDamageDealtToPlayerThisTurn = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_WeAreMovingAroundALot_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            minMovesThisTurn = 3,
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{braid}_OneHitPointThisIsFine_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                braid
            },
            oncePerCombatTags = new()
            {
                "aboutToDie"
            },
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_OneHitPointThisIsFine_1"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            oncePerCombatTags = new()
            {
                "aboutToDie"
            },
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_OneHitPointThisIsFine_2"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            oncePerCombatTags = new()
            {
                "aboutToDie"
            },
            oncePerRun = true,
            enemyShotJustHit = true,
            maxHull = 1,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_JustPlayedASashaCard_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            oncePerRunTags = new()
            {
                "usedASashaCard"
            },
            whoDidThat = Deck.sasha,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Braid_WentMissing_0"] = new()
        {
            type = NodeType.combat,
            priority = true,
            allPresent = new()
            {
                eili
            },
            lastTurnPlayerStatuses = new()
            {
                Instance.BraidChar.MissingStatus.Status
            },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{braid}_Eili_WentMissing_0"] = new()
        {
            type = NodeType.combat,
            priority = true,
            allPresent = new()
            {
                braid
            },
            lastTurnPlayerStatuses = new()
            {
                Instance.EiliChar.MissingStatus.Status
            },
            oncePerRun = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_WeDidOverFiveDamage_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 6,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                },
                new CustomSay()
                {
                    who = "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_ThatsALotOfDamageToThem_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            playerShotJustHit = true,
            minDamageDealtToEnemyThisAction = 10,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                },
                new CustomSay()
                {
                    who = "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{braid}_ResolveTriggered_0"] = new()
        {
            type = NodeType.combat,
            priority = true,
            allPresent = new()
            {
                braid
            },
            lookup = new()
            {
                "resolvetriggered"
            },
            oncePerCombat = true,
            lines = new()
            {
                new CustomSay()
                {
                    who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                    Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_IdentifyWeakpoint_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_identifyweakpoint_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_DisableDampeners_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_disableddampeners_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                    }
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_Hotwire_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_hotwire_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "loopTag"])) ? loopTag : "neutral",
                        },
                    }
                },
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice3", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice4", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice4", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "choice4", "loopTag"])) ? loopTag : "neutral",
                        },
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_AnchorShot_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_anchorshot_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_PickMeUp_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_pickmeup_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_Inspiration_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_inspiration_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_TargettingScramble_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_targettingscramble_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_ReroutePower_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_reroutepower_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
        DB.story.all[currentStory = $"{eili}_Card_DumpCargo_0"] = new()
        {
            type = NodeType.combat,
            allPresent = new()
            {
                eili
            },
            priority = true,
            oncePerCombat = true,
            oncePerRun = true,
            lookup = new()
            {
                "card_dumpcargo_played"
            },
            lines = new()
            {
                new SaySwitch()
                {
                    lines = new()
                    {
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice1", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice2", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice3", "loopTag"])) ? loopTag : "neutral",
                        },
                        new CustomSay()
                        {
                            who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice4", "who"]) ?? "crew",
                            Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice4", "what"]) ?? "...",
                            loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "choice4", "loopTag"])) ? loopTag : "neutral",
                        }
                    }
                }
            }
        };
    }
}
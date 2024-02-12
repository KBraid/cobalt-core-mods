using System.Linq;

namespace KBraid.BraidEili;

internal static class CombatDialogue
{
    private static ModEntry Instance => ModEntry.Instance;

    internal static void Inject()
    {
        string eili = Instance.EiliDeck.Deck.Key();
        string braid = Instance.BraidDeck.Deck.Key();
        string currentStory;

        // CRAB FACTS
        /*{
            DB.story.GetNode(currentStory = "CrabFacts1_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = braid,
                Text = "...",

            });
            DB.story.GetNode(currentStory = "CrabFacts2_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = braid,
                Text = "...",

            });
            DB.story.GetNode(currentStory = "CrabFactsAreOverNow_Multi_0")?.lines.OfType<SaySwitch>().LastOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = braid,
                Text = "...",

            });
        }*/
        // MID COMBAT SHOUTS
        {
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
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "serious"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "happy"
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
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "serious_c"
                    }
                }
            };
            DB.story.all[currentStory = $"{eili}_OneHitPointThisIsFine_0"] = new()
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "sad"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "sad"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "sad"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "happy"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "sad"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "shout"
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "happy"
                    },
                    new CustomSay()
                    {
                        who = "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
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
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "manic"
                    },
                    new CustomSay()
                    {
                        who = "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
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
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "serious_c"
                    }
                }
            };
        }
        // CARD LOOKUPS
        {
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
                            }
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "concerned"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
                            }
                        }
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "2", "what"])
                            }
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "concerned"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "concerned"
                            },
                            new CustomSay()
                            {
                                who =eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "3", "what"]),
                                loopTag = "concerned"
                            }
                        }
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "2", "what"])
                            },
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "3", "what"])
                            },
                            new CustomSay()
                            {
                                who = "crew",
                                Text = Instance.StoryLocs.Localize([currentStory, "2", "4", "what"])
                            }
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "happy"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "happy"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "3", "what"]),
                                loopTag = "happy"
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "determined"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "determined"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "3", "what"]),
                                loopTag = "determined"
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "3", "what"])
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"])
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
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
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "happy"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "happy"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "3", "what"]),
                                loopTag = "happy"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "4", "what"]),
                                loopTag = "happy"
                            }
                        }
                    }
                }
            };
        }
    }
}
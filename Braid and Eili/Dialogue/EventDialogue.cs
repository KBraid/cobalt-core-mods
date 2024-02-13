using System.Linq;

namespace KBraid.BraidEili;

internal static class EventDialogue
{
    private static ModEntry Instance => ModEntry.Instance;
    internal static void Inject()
    {
        string eili = Instance.EiliDeck.Deck.Key();
        string braid = Instance.BraidDeck.Deck.Key();
        string currentStory;

        // INSERT TO EXISTING EVENTS
        {
            DB.story.GetNode(currentStory = "GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = eili,
                Text = Instance.StoryLocs.Localize([currentStory, "eili", "1", "what"])
            });
            DB.story.GetNode(currentStory = "GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = braid,
                Text = Instance.StoryLocs.Localize([currentStory, "braid", "1", "what"])
            });
        }

        // NEW DIALOGUE FOR EXISTING EVENT CONDITIONS
        {
            DB.story.all[currentStory = $"ChoiceCardRewardOfYourColorChoice_{eili}"] = new()
            {
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = new()
                {
                    eili
                },
                bg = "BGBootSequence",
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
            DB.story.all[currentStory = $"ChoiceCardRewardOfYourColorChoice_{braid}"] = new()
            {
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = new()
                {
                    braid
                },
                bg = "BGBootSequence",
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
            DB.story.all[currentStory = $"LoseCharacterCard_{eili}"] = new()
            {
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = new()
                {
                    eili
                },
                bg = "BGSupernova",
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
                                loopTag = "sad"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "concerned"
                            }
                        }
                    }
                }
            };
            DB.story.all[currentStory = $"CrystallizedFriendEvent_{eili}"] = new()
            {
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = new()
                {
                    eili
                },
                bg = "BGCrystalizedFriend",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 1.5
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "sad"
                            },
                            new CustomSay()
                            {
                                who = eili,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"]),
                                loopTag = "happy"
                            }
                        }
                    }
                }
            };
            DB.story.all[currentStory = $"CrystallizedFriendEvent_{braid}"] = new()
            {
                type = NodeType.@event,
                oncePerRun = true,
                allPresent = new()
                {
                    braid
                },
                bg = "BGCrystalizedFriend",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 1.5
                    },
                    new SaySwitch()
                    {
                        lines = new()
                        {
                            new CustomSay()
                            {
                                who = braid,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "1", "what"]),
                                loopTag = "blink"
                            },
                            new CustomSay()
                            {
                                who = braid,
                                Text = Instance.StoryLocs.Localize([currentStory, "1", "2", "what"])
                            }
                        }
                    }
                }
            };
        }
        // NEW RUN WIN AND MEMORIES
        {
            DB.story.all[currentStory = $"{eili}_RunWin_1"] = new()
            {
                type = NodeType.@event,
                once = true,
                lookup = new()
                {
                    $"runWin_{eili}"
                },
                bg = "BGRunWin",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "concerned"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"]),
                        loopTag = "concerned"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_RunWin_1"] = new()
            {
                type = NodeType.@event,
                once = true,
                lookup = new()
                {
                    $"runWin_{braid}"
                },
                bg = "BGRunWin",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "7", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "9", "what"]),
                        loopTag = "serious"
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_RunWin_2"] = new()
            {
                type = NodeType.@event,
                once = true,
                lookup = new()
                {
                    $"runWin_{braid}"
                },
                requiredScenes = new()
                {
                    $"{braid}_RunWin_1"
                },
                bg = "BGRunWin",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "7", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                }
            };
            DB.story.all[currentStory = $"{braid}_RunWin_3"] = new()
            {
                type = NodeType.@event,
                once = true,
                lookup = new()
                {
                    $"runWin_{braid}"
                },
                requiredScenes = new()
                {
                    $"{braid}_RunWin_2"
                },
                bg = "BGRunWin",
                lines = new()
                {
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "7", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "8", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "9", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text = Instance.StoryLocs.Localize([currentStory, "10", "what"])
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_Memory_1"] = new()
            {
                type = NodeType.@event,
                introDelay = false,
                bg = "BGMemoryButCool",
                lookup = new()
                {
                    "vault",
                    $"vault_{braid}"
                },
                lines = new()
                {
                    new CustomTitle()
                    {
                        Text = "T-1084 days"
                    },
                    new Wait()
                    {
                        secs = 2
                    },
                    new CustomTitle()
                    {
                        empty = true
                    },
                    new Wait()
                    {
                        secs = 0.5
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "blink"
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "3", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "4", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "5", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        Text =  Instance.StoryLocs.Localize([currentStory, "7", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "9", "what"]),
                        loopTag = "serious_c"
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_Memory_2"] = new()
            {
                type = NodeType.@event,
                introDelay = false,
                bg = "BGBarren",
                lookup = new()
                {
                    "vault",
                    $"vault_{braid}"
                },
                requiredScenes = new()
                {
                    $"{braid}_Memory_1"
                },
                lines = new()
                {
                    new CustomTitle()
                    {
                        Text = "T-412 days"
                    },
                    new Wait()
                    {
                        secs = 2
                    },
                    new CustomTitle()
                    {
                        empty = true
                    },
                    new Wait()
                    {
                        secs = 0.5
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "4", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "concerned",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "7", "what"]),
                        loopTag = "concerned",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "9", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "10", "what"]),
                        loopTag = "concerned",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "11", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "12", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "13", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "14", "what"]),
                        loopTag = "serious_b"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "15", "what"]),
                        loopTag = "serious_a"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "16", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "11", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "18", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "19", "what"]),
                        loopTag = "serious_a"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "20", "what"]),
                        loopTag = "determined",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "21", "what"]),
                        loopTag = "determined"
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_Memory_3"] = new()
            {
                type = NodeType.@event,
                introDelay = false,
                bg = "BGVault",
                bgSetup = new()
                {
                    "dark_on",
                    "ambience_on"
                },
                lookup = new()
                {
                    "vault",
                    $"vault_{braid}"
                },
                requiredScenes = new()
                {
                    $"{braid}_Memory_2"
                },
                lines = new()
                {
                    new CustomTitle()
                    {
                        Text = "T-244 minutes"
                    },
                    new Wait()
                    {
                        secs = 2
                    },
                    new CustomTitle()
                    {
                        empty = true
                    },
                    new Wait()
                    {
                        secs = 0.5
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "concerned",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "4", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "6", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "7", "what"]),
                        loopTag = "happy",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "8", "what"]),
                        loopTag = "happy",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "9", "what"]),
                        loopTag = "eyes_closed"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "10", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "11", "what"]),
                        loopTag = "serious"
                    },
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomTitle()
                    {
                        empty = true
                    },
                    new CustomTitle()
                    {
                        Text = "T-3 minutes"
                    },
                    new SetBG()
                    {
                        bg = "BGDocking"
                    },
                    new BGAction()
                    {
                        action = "docking_on"
                    },
                    new Wait()
                    {
                        secs = 3
                    },
                    new CustomTitle()
                    {
                        empty = true
                    },
                    new Wait()
                    {
                        secs = 1
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "12", "what"]),
                        loopTag = "panic",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "13", "what"]),
                        loopTag = "serious_a"
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "14", "what"]),
                        loopTag = "squint",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "15", "what"]),
                        loopTag = "mad",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "16", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "17", "what"]),
                        loopTag = "squint",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "18", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text =  Instance.StoryLocs.Localize([currentStory, "19", "what"]),
                        loopTag = "happy"
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "20", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "21", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "22", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "23", "what"]),
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "24", "what"]),
                        loopTag = "unamused"
                    },
                    new BGAction()
                    {
                        action = "alarm_on"
                    },
                    new BGAction()
                    {
                        action = "critical_on"
                    },
                    new Wait()
                    {
                        secs = 1
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "25", "what"]),
                        loopTag = "panic",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "26", "what"]),
                        loopTag = "serious"
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "27", "what"]),
                        loopTag = "panic",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "28", "what"]),
                        loopTag = "squint",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "29", "what"]),
                        loopTag = "mad",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = "peri",
                        Text =  Instance.StoryLocs.Localize([currentStory, "30", "what"]),
                        loopTag = "mad",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = braid,
                        Text =  Instance.StoryLocs.Localize([currentStory, "31", "what"]),
                        loopTag = "serious_a"
                    },
                    new BGAction()
                    {
                        action = "riggs_on"
                    },
                    new Wait()
                    {
                        secs = 0.5
                    },
                    new BGAction()
                    {
                        action = "rumble_on"
                    },
                    new Wait()
                    {
                        secs = 5
                    },
                    new BGAction()
                    {
                        action = "title_card_on"
                    },
                    new CustomTitle()
                    {
                        Text = "<c=downside>T-0 seconds</c>"
                    },
                    new BGAction()
                    {
                        action = "kill_sound_on"
                    },
                    new Wait()
                    {
                        secs = 9
                    }
                }
            };
        }
        // START RUN
        {
            DB.story.all[currentStory = $"{eili}_CharStart_0"] = new()
            {
                type = NodeType.@event,
                priority = true,
                once = true,
                lookup = new()
                {
                    "zone_first"
                },
                allPresent = new()
                {
                    eili,
                },
                bg = "BGRunStart",
                lines = new()
                {
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"])
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "grumpy"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        loopTag = "happy"
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"]),
                        loopTag = "worried"
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "squint"
                    },
                    new CustomSay()
                    {
                        who = eili,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "7", "what"]),
                        loopTag = "squint"
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "9", "what"]),
                        loopTag = "happy"
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_CharStart_0"] = new()
            {
                type = NodeType.@event,
                priority = true,
                once = true,
                lookup = new()
                {
                    "zone_first"
                },
                allPresent = new()
                {
                    braid,
                },
                requiredScenes = new()
                {
                    $"{eili}_CharStart_0",
                },
                bg = "BGRunStart",
                lines = new()
                {
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "grumpy"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"]),
                        loopTag = "blink"
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        loopTag = "worried"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "4", "what"])
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "5", "what"]),
                        loopTag = "mad"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "6", "what"])
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "7", "what"]),
                        loopTag = "squint"
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "8", "what"])
                    },
                    new CustomSay()
                    {
                        who = braid,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "9", "what"])
                    },
                    new CustomSay()
                    {
                        who = "comp",
                        Text = Instance.StoryLocs.Localize([currentStory, "10", "what"]),
                        loopTag = "squint"
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}Eili_CharStart_0"] = new()
            {
                type = NodeType.@event,
                priority = true,
                once = true,
                lookup = new()
                {
                    "zone_first"
                },
                allPresent = new()
                {
                    braid,
                    eili
                },
                requiredScenes = new()
                {
                    $"{braid}_CharStart_0",
                },
                bg = "BGRunStart",
                lines = new()
                {
                    new CustomSay()
                    {
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "1", "what"]),
                        loopTag = "concerned"
                    },
                    new CustomSay()
                    {
                        who = braid,
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "2", "what"])
                    },
                    new CustomSay()
                    {
                        who = eili,
                        Text = Instance.StoryLocs.Localize([currentStory, "3", "what"]),
                        loopTag = "happy"
                    }
                }
            };
        }
    }
}
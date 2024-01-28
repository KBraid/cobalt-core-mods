using System.Collections.Generic;
using System.Linq;

namespace KBraid.BraidEili;

internal static class EventDialogue
{
    private static ModEntry Instance => ModEntry.Instance;
    internal static void Inject()
    {
        string eili = Instance.EiliDeck.UniqueName;
        string braid = Instance.BraidDeck.UniqueName;
        var currentStory = string.Empty;
        var loopTag = string.Empty;

        // INSERT TO EXISTING EVENTS
        {
            DB.story.GetNode(currentStory = "CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "eili", "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "eili", "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "eili", "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "braid", "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "braid", "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "braid", "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
        }

        // NEW DIALOGUE FOR EXISTING EVENT CONDITIONS
        {
            DB.story.all[currentStory = $"{eili}_ChoiceCardRewardOfYourColorChoice_0"] = new()
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_ChoiceCardRewardOfYourColorChoice_0"] = new()
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                    }
                }
            };
            DB.story.all[currentStory = $"{eili}_LoseCharacterCard_0"] = new()
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
            DB.story.all[currentStory = $"{eili}_CrystallizedFriendEvent_0"] = new()
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
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                    }
                }
            };
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
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]) ?? "...",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue4", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue4", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]) ?? "...",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue6", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue6", "loopTag"])) ? loopTag : "neutral",
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
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue3", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue3", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue9", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue9", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue9", "loopTag"])) ? loopTag : "neutral",
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
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue3", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue3", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue6", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue6", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue7", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue7", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"]) ?? "..."
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue3", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue3", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue4", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue4", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]) ?? "..."
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue7", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue7", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue8", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue8", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue9", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue9", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue9", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue10", "who"]) ?? "crew",
                        Text = Instance.StoryLocs.Localize([currentStory, "dialogue10", "what"]) ?? "...",
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue10", "loopTag"])) ? loopTag : "neutral",
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_Memory_1"] = new()
            {
                type = NodeType.@event,
                introDelay = false,
                bg = "BGVault",
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
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"])
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"])
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"])
                    },
                    new CustomSay()
                    {
                        who = "void",
                        flipped = true,
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"])
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue9", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue9", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue9", "loopTag"])) ? loopTag : "neutral",
                    }
                }
            };
            DB.story.all[currentStory = $"{braid}_Memory_2"] = new()
            {
                type = NodeType.@event,
                introDelay = false,
                bg = "BGVault",
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue3", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue3", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue4", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue4", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue6", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue6", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue7", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue7", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue8", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue8", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue9", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue9", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue9", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue10", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue10", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue10", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue11", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue11", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue11", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue12", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue12", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue12", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue13", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue13", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue13", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue14", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue14", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue14", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue15", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue15", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue15", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue16", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue16", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue16", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue17", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue11", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue11", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue18", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue18", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue18", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue19", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue19", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue19", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue20", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue20", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue20", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue21", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue21", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue21", "loopTag"])) ? loopTag : "neutral",
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue2", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue2", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue2", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue3", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue3", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue3", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue4", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue4", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue4", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue5", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue5", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue5", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue6", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue6", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue6", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue7", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue7", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue7", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue8", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue8", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue8", "loopTag"])) ? loopTag : "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue9", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue9", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue9", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue10", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue10", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue10", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue11", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue11", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue11", "loopTag"])) ? loopTag : "neutral",
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue12", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue12", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue12", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue13", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue13", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue13", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue14", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue14", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue14", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue15", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue15", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue15", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue16", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue16", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue16", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue17", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue17", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue17", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue18", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue18", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue18", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue19", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue19", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue19", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue20", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue20", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue20", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue21", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue21", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue21", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue22", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue22", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue22", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue23", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue23", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue23", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue24", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue24", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue24", "loopTag"])) ? loopTag : "neutral",
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
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue25", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue25", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue25", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue26", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue26", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue26", "loopTag"])) ? loopTag : "neutral",
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue27", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue27", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue27", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue28", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue28", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue28", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue29", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue29", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue29", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue30", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue30", "what"]),
                        loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue30", "loopTag"]) ?? "neutral",
                        flipped = true
                    },
                    new CustomSay()
                    {
                        who = Instance.StoryLocs.Localize([currentStory, "dialogue31", "who"]) ?? "crew",
                        Text =  Instance.StoryLocs.Localize([currentStory, "dialogue31", "what"]),
                        loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue31", "loopTag"])) ? loopTag : "neutral",
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
    }
}
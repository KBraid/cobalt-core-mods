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
        }
    }
}
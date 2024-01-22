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
            DB.story.GetNode(currentStory = "AbandonedShipyard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "AbandonedShipyard_Repaired")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "CrystallizedFriendEvent")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "DraculaTime")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "GrandmaShop")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
            });
            DB.story.GetNode(currentStory = "LoseCharacterCard")?.lines.OfType<SaySwitch>().FirstOrDefault()?.lines.Insert(0, new CustomSay()
            {
                who = Instance.StoryLocs.Localize([currentStory, "dialogue1", "who"]) ?? "crew",
                Text = Instance.StoryLocs.Localize([currentStory, "dialogue1", "what"]) ?? "...",
                loopTag = Instance.FaceSprites.Contains(loopTag = Instance.StoryLocs.Localize([currentStory, "dialogue1", "loopTag"])) ? loopTag : "neutral",
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
        }
    }
}
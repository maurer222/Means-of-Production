using System.Collections.Generic;

public class Goal
{
    public string GoalName { get; set; }
    public Dictionary<string, bool> Preconditions { get; set; }
    public Dictionary<string, bool> Effects { get; set; }

    public Goal(string name, Dictionary<string, bool> preconditions, Dictionary<string, bool> effects)
    {
        GoalName = name;
        Preconditions = preconditions;
        Effects = effects;
    }

    public bool IsAchieved(Dictionary<string, bool> state)
    {
        foreach (var effect in Effects)
        {
            if (!state.ContainsKey(effect.Key) || state[effect.Key] != effect.Value)
            {
                return false;
            }
        }
        return true;
    }
}
using System.Collections.Generic;
using System;

public class Action
{
    public string Name { get; private set; }
    public Dictionary<string, bool> Preconditions { get; private set; }
    public Dictionary<string, bool> Effects { get; private set; }
    public float Cost { get; private set; }
    public Func<Employee, bool> PerformAction { get; private set; }

    public Action(string name, Dictionary<string, bool> preconditions, Dictionary<string, bool> effects, float cost, Func<Employee, bool> performAction)
    {
        Name = name;
        Preconditions = preconditions;
        Effects = effects;
        Cost = cost;
        PerformAction = performAction;
    }

    public bool ArePreconditionsMet(Dictionary<string, bool> state)
    {
        foreach (var precondition in Preconditions)
        {
            if (!state.ContainsKey(precondition.Key) || state[precondition.Key] != precondition.Value)
            {
                return false;
            }
        }
        return true;
    }

    public bool Perform(Employee employee)
    {
        return PerformAction(employee);
    }
}
using System.Collections.Generic;

[System.Serializable]
public class Task
{
    public enum TaskState { 
        Available, 
        InProgress, 
        Completed }

    public TaskState State { get; private set; }
    public int MaxEmployees { get; private set; }
    public int CurrentEmployeeCount => currentEmployeeCount;
    public List<Goal> Goals { get; private set; }
    public List<Action> Actions { get; private set; }
    public int CurrentActionIndex { get; private set; }

    private int currentEmployeeCount;
    public int Priority;

    public Task(int maxEmployees, List<Goal> goals, List<Action> actions)
    {
        State = TaskState.Available;
        MaxEmployees = maxEmployees;
        currentEmployeeCount = 0;
        Goals = goals;
        Actions = actions;
        CurrentActionIndex = 0;
    }

    public bool CanAssignEmployee()
    {
        return State != TaskState.Completed && currentEmployeeCount < MaxEmployees;
    }

    public void AssignEmployee()
    {
        if (CanAssignEmployee())
        {
            currentEmployeeCount++;
            if (currentEmployeeCount == MaxEmployees)
            {
                State = TaskState.InProgress;
            }
        }
    }

    public void CompleteTask()
    {
        State = TaskState.Completed;
    }

    public void IncrementActionIndex()
    {
        CurrentActionIndex++;
    }
}
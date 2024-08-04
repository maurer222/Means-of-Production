using System.Collections;
using UnityEngine;

[System.Serializable]
public class Task
{
    public enum TaskType {
        UnlockTruck,
        PutawayFromReceiving,
        PickForProcessing,
        PutawayFromProcessing,
        PickForShipping,
        LoadTruck
    }

    public TaskType Type { get; private set; }
    public int Quantity { get; private set; }
    public float Duration { get; private set; }
    public int Priority { get; private set; }

    public string Description;

    public Task(TaskType type, int quantity, float duration, int priority)
    {
        Type = type;
        Quantity = quantity;
        Duration = duration;
        Priority = priority;
        Description = type.ToString();
    }
}
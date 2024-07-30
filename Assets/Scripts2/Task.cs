using System.Collections;
using UnityEngine;

public class Task
{
    public enum TaskType
    {
        MoveMaterial,
        ProcessMaterial,
        ShipMaterial
    }

    public TaskType Type { get; private set; }
    public int Priority { get; private set; }
    public float Duration { get; private set; }
    public bool IsCompleted { get; set; }
    // Add more properties as needed

    public Task(TaskType type, int priority, float duration)
    {
        Type = type;
        Priority = priority;
        Duration = duration;
        IsCompleted = false;
    }

    public void PerformTask()
    {
        IsCompleted = true;
    }
}
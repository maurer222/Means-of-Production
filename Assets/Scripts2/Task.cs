using System.Collections;
using UnityEngine;

public class Task
{
    public enum TaskType { MoveMaterial, 
                           ProcessMaterial, 
                           LoadTruck, 
                           UnloadTruck }
    public TaskType Type { get; private set; }
    public int Quantity { get; private set; }
    public float Duration { get; private set; }
    public int Priority { get; private set; }
    public int Destination { get; private set; }

    public Task(TaskType type, int quantity, float duration, int priority)
    {
        Type = type;
        Quantity = quantity;
        Duration = duration;
        Priority = priority;
    }
}
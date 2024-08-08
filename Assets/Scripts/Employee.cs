using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Employee : MonoBehaviour
{
    [SerializeField] private GameObject palletSpawnPoint;

    public List<Goal> Goals { get; set; }
    public List<Action> Actions { get; set; }
    public Dictionary<string, bool> State { get; set; }
    public bool IsAvailable => CurrentTask == null;

    public Task CurrentTask;
    public int EmployeeID;
    public string Name;
    public NavMeshAgent agent;
    private InventoryManager inventoryManager;
    private ProcessingManager processingManager;

    void Start()
    {
        Goals = new List<Goal>();
        Actions = new List<Action>(); 
        State = new Dictionary<string, bool>
        {
            { "TruckArrived", true }, // Assuming the truck has arrived for testing purposes
            { "EquipmentAvailable", true } // Assuming equipment is available for testing purposes
        };

        agent = GetComponent<NavMeshAgent>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        processingManager = FindFirstObjectByType<ProcessingManager>();
        TaskManager.Instance.RegisterEmployee(this);
    }

    public void AssignTask(Task task)
    {
        CurrentTask = task;
        StartCoroutine(ExecuteTask());
    }

    private IEnumerator ExecuteTask()
    {
        while (CurrentTask != null && CurrentTask.CurrentActionIndex < CurrentTask.Actions.Count)
        {
            var goal = CurrentTask.Goals.FirstOrDefault(g => g.Preconditions.All(p => State.ContainsKey(p.Key) && State[p.Key] == p.Value));
            if (goal == null)
            {
                Debug.LogError("No goal found that matches current state.");
                CurrentTask = null;
                yield break;
            }

            var action = CurrentTask.Actions[CurrentTask.CurrentActionIndex];
            if (action.ArePreconditionsMet(State))
            {
                if (action.Perform(this))
                {
                    foreach (var effect in action.Effects)
                    {
                        State[effect.Key] = effect.Value;
                    }

                    CurrentTask.IncrementActionIndex();

                    if (goal.IsAchieved(State))
                    {
                        Debug.Log("Goal achieved: " + goal.GoalName);
                        CurrentTask.CompleteTask();
                        TaskManager.Instance.UpdateTaskState(CurrentTask);
                        CurrentTask = null;
                        TaskManager.Instance.AssignTasks();
                        yield break;
                    }

                    // Wait for a frame to ensure other systems can update
                    yield return null;
                }
            }

            // Wait for a frame to ensure other systems can update
            yield return null;
        }
    }

    public bool ReserveForklift()
    {
        // Logic to reserve a forklift
        Debug.Log("Forklift Reserved");
        State["ForkliftReserved"] = true;
        return true;
    }

    public bool MoveToTruck()
    {
        // Logic to move to truck
        Debug.Log("Moved To Truck");
        State["AtTruck"] = true;
        return true;
    }

    public bool PickUpPallet()
    {
        // Logic to pick up pallet
        Debug.Log("Pallet Picked Up");
        State["PalletPicked"] = true;
        return true;
    }

    public bool MoveToReceiving()
    {
        // Logic to move to receiving area
        Debug.Log("Moved To Receiving");
        State["AtReceiving"] = true;
        return true;
    }

    public bool PlaceInReceiving()
    {
        // Logic to place pallet in receiving area
        Debug.Log("Pallet Placed in Receiving");
        State["PalletInReceiving"] = true;
        return true;
    }
}
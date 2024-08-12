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
    public NavMeshAgent agent;
    public int EmployeeID;
    public string Name;

    private int currentLocationIndex;
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
        BuildTaskPath();
        StartCoroutine(ExecuteTask());
    }

    private IEnumerator ExecuteTask()
    {
        while (CurrentTask != null && CurrentTask.CurrentActionIndex < CurrentTask.Actions.Count)
        {
            // Check if the current destination is still valid
            if (CurrentTask.TravelLocations != null && currentLocationIndex < CurrentTask.TravelLocations.Count)
            {
                Transform currentDestination = CurrentTask.TravelLocations[currentLocationIndex];

                // Validate the destination (e.g., check if storage is full)
                if (!IsDestinationValid(currentDestination))
                {
                    // Recalculate the path or choose a new destination
                    currentDestination = RecalculateDestination();
                    if (currentDestination == null)
                    {
                        // Handle the case where no valid destination is found
                        Debug.LogWarning("No valid destination found.");
                        break;
                    }
                }

                // Set the agent's destination
                agent.SetDestination(currentDestination.position);

                // Wait until the employee reaches the destination
                while (agent.remainingDistance > agent.stoppingDistance)
                {
                    yield return null;
                }

                currentLocationIndex++; // Move to the next location
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

                    // Wait for a frame to ensure other systems can update
                    yield return null;
                }
            }

            // Wait for a frame to ensure other systems can update
            yield return null;
        }

        CurrentTask = null;
    }

    private void BuildTaskPath()
    {
        if (CurrentTask == null) return;


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
        bool added = inventoryManager.AddMaterialsToStorage(StorageArea.StorageType.Receiving, 1);
        if (added)
        {
            Debug.Log("Pallet Placed in Receiving");
            State["PalletInReceiving"] = true;
            return true;
        }
        else
        {
            Debug.Log("Receiving area is full.");
            return false;
        }
    }

    private bool IsDestinationValid(Transform destination)
    {
        // Example: Check if the storage area is full
        // This can be extended to include other validation logic as needed
        if (destination.CompareTag("ReceivingArea"))
        {
            var storageArea = destination.GetComponent<StorageArea>();
            return storageArea != null && storageArea.AvailableCapacity() > 0;
        }
        return true; // If no specific validation needed, return true
    }

    private Transform RecalculateDestination()
    {
        // Logic to find a new valid destination
        // Example: Find a storage area with available capacity
        foreach (var area in inventoryManager.ReceivingAreas)
        {
            if (area.AvailableCapacity() > 0)
            {
                return area.transform;
            }
        }
        // Return null if no valid destination is found
        return null;
    }
}
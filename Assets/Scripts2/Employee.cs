using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Task;

public class Employee : MonoBehaviour
{
    [SerializeField] private GameObject palletSpawnPoint;

    public bool isAvailable { get; set; } = true;

    public int EmployeeID;
    public string Name;
    public Task CurrentTask;
    public NavMeshAgent agent;
    private InventoryManager inventoryManager;
    private ProcessingManager processingManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inventoryManager = FindFirstObjectByType<InventoryManager>();
        processingManager = FindFirstObjectByType<ProcessingManager>();
        TaskManager.Instance.RegisterEmployee(this);
    }

    public void AssignTask(Task task)
    {
        CurrentTask = task;
        isAvailable = false;
        StartCoroutine(PerformTask());
    }

    private IEnumerator PerformTask()
    {
        Vector3 taskStartLocation = GetTaskStartLocation(CurrentTask.Type);
        Vector3 taskEndLocation = GetTaskEndLocation(CurrentTask.Type);

        // Move to the start location
        agent.SetDestination(taskStartLocation);

        // Wait for the path to be calculated
        while (agent.pathPending)
        {
            yield return null;
        }

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        palletSpawnPoint.SetActive(true);

        // Simulate the task duration at the start location (e.g., picking up an item)
        yield return new WaitForSeconds(CurrentTask.Duration / 2);

        // Move to the end location
        agent.SetDestination(taskEndLocation);

        // Wait for the path to be calculated
        while (agent.pathPending)
        {
            yield return null;
        }

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        palletSpawnPoint.SetActive(false);

        // Simulate the task duration at the end location (e.g., dropping off an item)
        yield return new WaitForSeconds(CurrentTask.Duration / 2);

        CompleteTask();
    }

    private Vector3 GetTaskStartLocation(Task.TaskType taskType)
    {
        switch (taskType)
        {
            case Task.TaskType.UnlockTruck:
                return inventoryManager.receivingDocks[0].position;
            case Task.TaskType.PutawayFromReceiving:
                return inventoryManager.receivingAreas[0].position;
            case Task.TaskType.PickForProcessing:
                return inventoryManager.storageAreas[0].position;
            case Task.TaskType.PutawayFromProcessing:
                return processingManager.machines[0].transform.position;
            case Task.TaskType.PickForShipping:
                return inventoryManager.storageAreas[0].position;
            case Task.TaskType.LoadTruck:
                return inventoryManager.loadingAreas[0].position;
            default:
                return Vector3.zero;
        }
    }

    private Vector3 GetTaskEndLocation(Task.TaskType taskType)
    {
        switch (taskType)
        {
            case Task.TaskType.UnlockTruck:
                return inventoryManager.receivingAreas[0].position;
            case Task.TaskType.PutawayFromReceiving:
                return inventoryManager.storageAreas[0].position;
            case Task.TaskType.PickForProcessing:
                return processingManager.machines[0].transform.position;
            case Task.TaskType.PutawayFromProcessing:
                return inventoryManager.storageAreas[0].position;
            case Task.TaskType.PickForShipping:
                return inventoryManager.loadingAreas[0].position;
            case Task.TaskType.LoadTruck:
                return inventoryManager.loadingDocks[0].position;
            default:
                return Vector3.zero;
        }
    }

    private void CompleteTask()
    {
        CurrentTask = null;
        isAvailable = true;
        TaskManager.Instance.AssignTasks();
    }
}
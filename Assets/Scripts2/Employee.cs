using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static Task;

public class Employee : MonoBehaviour
{
    public bool isAvailable { get; set; } = true;

    public int EmployeeID;
    public string Name;
    public Task CurrentTask;
    public NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
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
        Vector3 taskLocation = GetTaskLocation(CurrentTask.Type);
        agent.SetDestination(taskLocation);

        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        yield return new WaitForSeconds(CurrentTask.Duration);
        CompleteTask();
    }

    private Vector3 GetTaskLocation(Task.TaskType taskType)
    {
        switch (taskType)
        {
            case Task.TaskType.MoveMaterialToReceiving:
                return InventoryManager.receivingDocks[0].position;
            case Task.TaskType.MoveMaterialToStorage:
                return InventoryManager.receivingAreas[0].position;
            case Task.TaskType.MoveMaterialToMachine:
                return ProcessingManager.machines[0].transform.position;
            case Task.TaskType.MoveProcessedMaterialToStorage:
                return InventoryManager.storageAreas[0].position;
            case Task.TaskType.MoveMaterialToLoading:
                return InventoryManager.loadingAreas[0].position;
            case Task.TaskType.MoveMaterialToTruck:
                return InventoryManager.loadingDocks[0].position;
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
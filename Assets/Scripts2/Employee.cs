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
        // Move to the task location
        Vector3 taskLocation = GetTaskLocation(CurrentTask.Type);
        agent.SetDestination(taskLocation);

        // Wait until the employee reaches the task location
        while (agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        // Perform the task
        yield return new WaitForSeconds(CurrentTask.Duration);
        CompleteTask();
    }

    private Vector3 GetTaskLocation(TaskType taskType)
    {
        // Define task locations based on task type
        switch (taskType)
        {
            case TaskType.MoveMaterial:
                return new Vector3(0, 0, 0); // Example location
            case TaskType.ProcessMaterial:
                return new Vector3(10, 0, 0); // Example location
            case TaskType.LoadTruck:
                return new Vector3(20, 0, 0); // Example location
            case TaskType.UnloadTruck:
                return new Vector3(20, 0, 0); // Example location
            default:
                return Vector3.zero;
        }
    }

    private void CompleteTask()
    {
        CurrentTask = null;
        isAvailable = true;
        TaskManager.Instance.TaskCompleted(this);
        Debug.Log( CurrentTask.Type.ToString() + " complete");
        // Trigger task completion events or logic here
    }
}
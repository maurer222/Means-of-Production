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
        //while (Mathf.Abs(Vector3.Distance(transform.position, agent.destination)) > agent.stoppingDistance)
        //{
        //    yield return null;
        //}

        // Perform the task
        yield return new WaitForSeconds(CurrentTask.Duration);
        Debug.Log("Task complete for " + name);
        CompleteTask();
    }

    private Vector3 GetTaskLocation(TaskType taskType)
    {
        // Define task locations based on task type
        switch (taskType)
        {
            case TaskType.MoveMaterial:
                return transform.position + new Vector3(Random.Range(-5, 5.1f), 0, Random.Range(-5, 5.1f));
            case TaskType.ProcessMaterial:
                return transform.position + new Vector3(Random.Range(-5, 5.1f), 0, Random.Range(-5, 5.1f));
            case TaskType.LoadTruck:
                return transform.position + new Vector3(Random.Range(-5, 5.1f), 0, Random.Range(-5, 5.1f));
            case TaskType.UnloadTruck:
                return transform.position + new Vector3(Random.Range(-5, 5.1f), 0, Random.Range(-5, 5.1f));
            default:
                return Vector3.zero;
        }
    }

    private void CompleteTask()
    {
        TaskManager.Instance.TaskCompleted(this);
        CurrentTask = null;
        isAvailable = true;
        // Trigger task completion events or logic here
    }
}
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;

    public List<Task> availableTasks = new List<Task>();
    public List<Task> inProgressTasks = new List<Task>();
    public List<Task> completedTasks = new List<Task>();
    public List<Employee> employees = new List<Employee>();

    private List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
    private List<SalesOrder> salesOrders = new List<SalesOrder>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddTask(Task task)
    {
        availableTasks.Add(task);
        AssignTasks();
    }

    private void SortTaskQueue()
    {
        availableTasks.Sort((x, y) => y.Priority.CompareTo(x.Priority));
        inProgressTasks.Sort((x, y) => y.Priority.CompareTo(x.Priority));
    }

    public void AssignTasks()
    {
        foreach (var employee in employees)
        {
            bool assigned = false;

            // Prioritize in-progress tasks that can accept more employees
            foreach (var task in inProgressTasks)
            {
                if (task.CanAssignEmployee())
                {
                    task.AssignEmployee();
                    employee.AssignTask(task);
                    assigned = true;
                    break;
                }
            }

            // If no in-progress tasks available, check available tasks
            if (!assigned)
            {
                foreach (var task in availableTasks)
                {
                    if (task.CanAssignEmployee())
                    {
                        task.AssignEmployee();
                        employee.AssignTask(task);
                        if (task.State == Task.TaskState.InProgress)
                        {
                            availableTasks.Remove(task);
                            inProgressTasks.Add(task);
                        }
                        break;
                    }
                }
            }
        }
    }

    public void UpdateTaskState(Task task)
    {
        if (task.State == Task.TaskState.Completed)
        {
            inProgressTasks.Remove(task);
            completedTasks.Add(task);
        }
    }

    public void MonitorAndReassignTasks()
    {
        foreach (var task in inProgressTasks)
        {
            if (task.State == Task.TaskState.AwaitingReassignment)
            {
                // Logic to find a new path or reassign the task
                RecalculatePathOrAssignNewTask(task);
            }
        }
    }

    private void RecalculatePathOrAssignNewTask(Task task)
    {
        // Recalculate the path based on current conditions
        // Reassign employees or split the task if needed
    }

    public void TaskCompleted(Employee employee)
    {
        AssignTasks();
    }

    void Update()
    {
        AssignTasks();
    }

    public void RegisterEmployee(Employee emp)
    {
        employees.Add(emp);
    }
}
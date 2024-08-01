using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    public List<Task> taskQueue = new List<Task>();
    public List<Employee> employees = new List<Employee>();
    private List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
    private List<SalesOrder> salesOrders = new List<SalesOrder>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddTask(Task task)
    {
        taskQueue.Add(task);
        taskQueue.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        Debug.Log(task.Type.ToString() + " in queue");
        AssignTasks();
    }

    private void AssignTasks()
    {
        foreach (Employee employee in employees)
        {
            if (employee.isAvailable && employee.agent != null && taskQueue.Count > 0)
            {
                Task nextTask = taskQueue[0];
                taskQueue.RemoveAt(0);
                employee.AssignTask(nextTask);
                Debug.Log(nextTask.Type.ToString() + " assigned");
            }
        }
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

    public void CreatePurchaseOrder(PurchaseOrder order)
    {
        purchaseOrders.Add(order);
        foreach (var task in order.Tasks)
        {
            AddTask(task);
        }
    }

    public void CreateSalesOrder(SalesOrder order)
    {
        salesOrders.Add(order);
        foreach (var task in order.Tasks)
        {
            AddTask(task);
        }
    }
}
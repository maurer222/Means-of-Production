using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private List<Task> taskQueue = new List<Task>();
    public List<Employee> employees = new List<Employee>();
    private List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
    private List<SalesOrder> salesOrders = new List<SalesOrder>();

    public void AddTask(Task task)
    {
        taskQueue.Add(task);
        taskQueue.Sort((x, y) => x.Priority.CompareTo(y.Priority));
        AssignTasks();
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

    private void AssignTasks()
    {
        foreach (Employee employee in employees)
        {
            if (employee.CurrentTask == null && taskQueue.Count > 0)
            {
                Task nextTask = taskQueue[0];
                taskQueue.RemoveAt(0);
                employee.AssignTask(nextTask);
            }
        }
    }

    void Update()
    {
        AssignTasks();
    }
}
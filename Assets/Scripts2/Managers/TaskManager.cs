using System.Collections.Generic;
using System.Linq;
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
        SortTaskQueue();
        AssignTasks();
        //Debug.Log(task.Type.ToString() + " in queue");
    }

    private void SortTaskQueue()
    {
        taskQueue.Sort((x, y) => y.Priority.CompareTo(x.Priority));
    }

    public void AssignTasks()
    {
        foreach (var employee in employees)
        {
            if (employee.isAvailable && taskQueue.Count > 0)
            {
                var task = taskQueue[0];
                taskQueue.RemoveAt(0);
                employee.AssignTask(task);
                Debug.Log(task.Type.ToString() + " assigned");
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
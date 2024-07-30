using UnityEngine;
using System.Collections.Generic;

public class EmployeeManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager FinancialManager;
    [SerializeField] private List<GameObject> employeePrefabs = new List<GameObject>();
    [SerializeField] private Transform employeeParent;

    public List<GameObject> employees = new List<GameObject>();
    public Queue<Task> taskQueue = new Queue<Task>();
    public float hireCost = 500.0f;

    public void HireEmployee()
    {
        if (FinancialManager.Funds >= hireCost)
        {
            FinancialManager.RemoveFunds(hireCost);
            GameObject employee = Instantiate(employeePrefabs[Random.Range(0, employeePrefabs.Count)], employeeParent.transform.position, Quaternion.identity) as GameObject;
            employee.transform.parent = employeeParent;
            Employee newEmployee = employee.GetComponent<Employee>();
            newEmployee.EmployeeID = employees.Count + 1;
            newEmployee.Name = "Employee " + newEmployee.EmployeeID;
            //newEmployee.SkillLevel = 1;
            employees.Add(employee);
            Debug.Log("Hired: " + newEmployee.Name);
        }
        else
        {
            Debug.Log("Not enough funds to hire an employee.");
        }
    }

    public void AddTaskToQueue(Task task)
    {
        taskQueue.Enqueue(task);
        //Debug.Log("Adding " + task.TaskName + " to queue");
    }

    private void AssignTasks()
    {
        Employee employeeScript;

        foreach (GameObject employee in employees)
        {
            employeeScript = employee.GetComponent<Employee>();

            if (employeeScript.CurrentTask == null && taskQueue.Count > 0)
            {
                Task nextTask = taskQueue.Dequeue();
                employeeScript.AssignTask(nextTask);
                //Debug.Log("Assigning " + nextTask.TaskName + " to " + employee.name);
            }
        }
    }

    void Update()
    {
        AssignTasks();
    }
}
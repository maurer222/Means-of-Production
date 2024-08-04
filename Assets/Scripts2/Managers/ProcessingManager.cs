using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private float ProcessingCost = 0.21f;

    [SerializeField] public List<GameObject> machines;

    public void ProcessMaterials(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (financialManager.Funds > ProcessingCost && inventoryManager.RawMaterials > 0)
            {
                financialManager.RemoveFunds(ProcessingCost);
                Task moveToMachine = new Task(Task.TaskType.PickForProcessing, 1, 5f, 70);
                Task processMaterial = new Task(Task.TaskType.PutawayFromProcessing, 1, 5f, 60);
                taskManager.AddTask(moveToMachine);
                taskManager.AddTask(processMaterial);
                inventoryManager.RemoveRawMaterials(1);
                inventoryManager.AddProcessedMaterials(1);
            }
            else
            {
                Debug.Log("Not enough funds or raw materials to continue processing!");
                break;
            }
        }
    }

    public void ProcessAllMaterials()
    {
        int processingTarget = inventoryManager.RawMaterials;
        ProcessMaterials(processingTarget);
    }

    //public void ProcessMaterials(int amount)
    //{
    //    for (int i = 0; i < amount; i++)
    //    {
    //        if (financialManager.Funds > ProcessingCost && inventoryManager.RawMaterials > 0)
    //        {
    //            financialManager.RemoveFunds(ProcessingCost);
    //            inventoryManager.AddProcessedMaterials(1);
    //            inventoryManager.RemoveRawMaterials(1);
    //            taskManager.AddTask(new Task(Task.TaskType.ProcessMaterial, 1, 5f, 25));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough funds to continue processing!");
    //            break;
    //        }
    //    }
    //}

    //public void ProcessAllMaterials()
    //{
    //    int processingTarget = inventoryManager.RawMaterials;

    //    for (int i = 0; i < processingTarget; i++) 
    //    { 
    //        if (financialManager.Funds > ProcessingCost && inventoryManager.RawMaterials > 0)
    //        {
    //            financialManager.RemoveFunds(ProcessingCost);
    //            inventoryManager.AddProcessedMaterials(1);
    //            inventoryManager.RemoveRawMaterials(1);
    //            taskManager.AddTask(new Task(Task.TaskType.ProcessMaterial, 1, 5f, 25));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough funds to continue processing!");
    //            break;
    //        }
    //    }
    //}
}
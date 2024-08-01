using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private float ProcessedMaterialsPrice = 26.37f;
    [SerializeField] private TMP_InputField salesInput;

    public void SellProcessedMaterials()
    {
        for (int i = 0; i < int.Parse(salesInput.text); i++)
        {
            if (inventoryManager.ProcessedMaterials > 0)
            {
                financialManager.AddFunds(ProcessedMaterialsPrice);
                inventoryManager.RemoveProcessedMaterials(1);
                taskManager.AddTask(new Task(Task.TaskType.LoadTruck, 1, 5f, 75));
            }
            else
            {
                Debug.Log("Not enough processed materials to continue!");
                break;
            }
        }
    }

    public void SellAllMaterials()
    {
        int targetSales = inventoryManager.ProcessedMaterials;

        for (int i = 0; i < targetSales; i++)
        {
            if (inventoryManager.ProcessedMaterials > 0)
            {
                financialManager.AddFunds(ProcessedMaterialsPrice);
                inventoryManager.RemoveProcessedMaterials(1);
                taskManager.AddTask(new Task(Task.TaskType.LoadTruck, 1, 5f, 75));
            }
            else
            {
                Debug.Log("Not enough processed materials to continue!");
                break;
            }
        }
    }

}

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

    public void CreateSelectedSalesOrder()
    {
        int amount = int.Parse(salesInput.text);
        int totalProcessedMaterials = inventoryManager.ProcessedMaterials;
        int numShipments = Mathf.CeilToInt(amount / 20);

        for (int i = 0; i < numShipments; i++)
        {
            int materialsToShip = Mathf.Min(amount, 20);
            if (totalProcessedMaterials >= materialsToShip)
            {
                for (int j = 0; j < materialsToShip; j++)
                {
                    Task moveToLoading = new Task(Task.TaskType.MoveMaterialToLoading, 1, 5f, 1);
                    Task moveToTruck = new Task(Task.TaskType.MoveMaterialToTruck, 1, 5f, 2);
                    taskManager.AddTask(moveToLoading);
                    taskManager.AddTask(moveToTruck);
                    inventoryManager.RemoveProcessedMaterials(1);
                    financialManager.AddFunds(ProcessedMaterialsPrice);
                }
                amount -= materialsToShip;
            }
            else
            {
                Debug.Log("Not enough processed materials to fulfill the order!");
                break;
            }

            Debug.Log("Shipment created");
        }
    }

    public void CreateMaxSalesOrder()
    {
        int totalProcessedMaterials = inventoryManager.ProcessedMaterials;
        int numShipments = Mathf.CeilToInt(totalProcessedMaterials / 20);

        for (int i = 0; i < numShipments; i++)
        {
            int materialsToShip = Mathf.Min(totalProcessedMaterials, 20);
            if (totalProcessedMaterials >= materialsToShip)
            {
                for (int j = 0; j < materialsToShip; j++)
                {
                    Task moveToLoading = new Task(Task.TaskType.MoveMaterialToLoading, 1, 5f, 1);
                    Task moveToTruck = new Task(Task.TaskType.MoveMaterialToTruck, 1, 5f, 2);
                    taskManager.AddTask(moveToLoading);
                    taskManager.AddTask(moveToTruck);
                    inventoryManager.RemoveProcessedMaterials(1);
                    financialManager.AddFunds(ProcessedMaterialsPrice);
                }
                totalProcessedMaterials -= materialsToShip;
            }
            else
            {
                Debug.Log("Not enough processed materials to fulfill the order!");
                break;
            }

            Debug.Log("Shipment created");
        }
    }

    //public void SellProcessedMaterials()
    //{
    //    for (int i = 0; i < int.Parse(salesInput.text); i++)
    //    {
    //        if (inventoryManager.ProcessedMaterials > 0)
    //        {
    //            financialManager.AddFunds(ProcessedMaterialsPrice);
    //            inventoryManager.RemoveProcessedMaterials(1);
    //            taskManager.AddTask(new Task(Task.TaskType.LoadTruck, 1, 5f, 75));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough processed materials to continue!");
    //            break;
    //        }
    //    }
    //}

    //public void SellAllMaterials()
    //{
    //    int targetSales = inventoryManager.ProcessedMaterials;

    //    for (int i = 0; i < targetSales; i++)
    //    {
    //        if (inventoryManager.ProcessedMaterials > 0)
    //        {
    //            financialManager.AddFunds(ProcessedMaterialsPrice);
    //            inventoryManager.RemoveProcessedMaterials(1);
    //            taskManager.AddTask(new Task(Task.TaskType.LoadTruck, 1, 5f, 75));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough processed materials to continue!");
    //            break;
    //        }
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class PurchasingManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Transform receivingArea;
    [SerializeField] private float RawMaterialCost = 18.21f;
    [SerializeField] private TMP_InputField purchasingInput;

    //public void PurchaseSelectedMaterials()
    //{
    //    for (int i = 0; i < int.Parse(purchasingInput.text); i++)
    //    {
    //        if (financialManager.Funds > RawMaterialCost)
    //        {
    //            inventoryManager.AddRawMaterials(1);
    //            financialManager.RemoveFunds(RawMaterialCost);
    //            taskManager.AddTask(new Task(Task.TaskType.MoveMaterial, 1, 5f, 50));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough funds to purchase!");
    //            break;
    //        }
    //    }
    //}

    //public void PurchaseMaxMaterials()
    //{
    //    int totalPurchased = (int)(financialManager.Funds / RawMaterialCost);

    //    for (int i = 0; i < totalPurchased; i++)
    //    {
    //        if (financialManager.Funds > RawMaterialCost)
    //        {
    //            inventoryManager.AddRawMaterials(1);
    //            financialManager.RemoveFunds(RawMaterialCost);
    //            taskManager.AddTask(new Task(Task.TaskType.MoveMaterial, 1, 5f, 50));
    //        }
    //        else
    //        {
    //            Debug.Log("Not enough funds to purchase!");
    //            break;
    //        }
    //    }
    //}

    public void CreatePurchaseOrder(int amount)
    {
        int numShipments = Mathf.CeilToInt((float)amount / 20);

        for (int i = 0; i < numShipments; i++)
        {
            int materialsToPurchase = Mathf.Min(amount, 20);
            if (financialManager.Funds >= materialsToPurchase * RawMaterialCost)
            {
                financialManager.RemoveFunds(materialsToPurchase * RawMaterialCost);
                for (int j = 0; j < materialsToPurchase; j++)
                {
                    Task moveToReceiving = new Task(Task.TaskType.MoveMaterialToReceiving, 1, 5f, 1);
                    Task moveToStorage = new Task(Task.TaskType.MoveMaterialToStorage, 1, 5f, 2);
                    taskManager.AddTask(moveToReceiving);
                    taskManager.AddTask(moveToStorage);
                    inventoryManager.AddRawMaterials(1);
                }
                amount -= materialsToPurchase;
            }
            else
            {
                Debug.Log("Not enough funds to complete the purchase order!");
                break;
            }
        }
    }
}

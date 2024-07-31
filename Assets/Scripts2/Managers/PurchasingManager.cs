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
    [SerializeField] private Transform receivingArea;
    [SerializeField] private float RawMaterialCost = 18.21f;
    [SerializeField] private TMP_InputField purchasingInput;

    //public void BuyRawMaterials()
    //{
    //    if(financialManager.Funds > RawMaterialCost)
    //    {
    //        inventoryManager.AddRawMaterials(1);
    //        financialManager.RemoveFunds(RawMaterialCost);
    //        employeeManager.AddTaskToQueue(new Task(Task.TaskType.MoveMaterial, 1, 5f));
    //    }
    //}

    public void PurchaseMaxMaterials()
    {
        if (financialManager.Funds > RawMaterialCost)
        {
            int totalPurchased = (int)(financialManager.Funds / RawMaterialCost);
            int totalShipments = (int)Mathf.Floor(totalPurchased / 20);

            inventoryManager.AddRawMaterials(totalPurchased);
            financialManager.RemoveFunds(RawMaterialCost * totalPurchased);
            employeeManager.AddTaskToQueue(new Task(Task.TaskType.MoveMaterial, 1, 5f));
        }
    }

    public void PurchaseSelectedMaterials()
    {
        if (financialManager.Funds > (RawMaterialCost * int.Parse(purchasingInput.text)))
        {
            int totalShipments = (int)Mathf.Floor(int.Parse(purchasingInput.text) / 20);

            inventoryManager.AddRawMaterials(int.Parse(purchasingInput.text));
            financialManager.RemoveFunds(RawMaterialCost * int.Parse(purchasingInput.text));
            employeeManager.AddTaskToQueue(new Task(Task.TaskType.MoveMaterial, 1, 5f));
        }
        else
        {
            Debug.Log("Not enough funds to buy " + int.Parse(purchasingInput.text) + " Raw Materials");
        }
    }
}

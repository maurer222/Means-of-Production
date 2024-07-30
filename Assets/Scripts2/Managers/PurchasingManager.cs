using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private Transform receivingArea;
    [SerializeField] private float RawMaterialCost = 18.21f;

    public void BuyRawMaterials()
    {
        if(financialManager.Funds > RawMaterialCost)
        {
            inventoryManager.AddRawMaterials(1);
            financialManager.RemoveFunds(RawMaterialCost);
            employeeManager.AddTaskToQueue(new Task(Task.TaskType.MoveMaterial, 1, 5f));
        }
    }
}

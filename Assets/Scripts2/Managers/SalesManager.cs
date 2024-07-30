using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager FinancialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private float ProcessedMaterialsPrice = 26.37f;

    public void SellProcessedMaterials(int amount)
    {
        if (inventoryManager.ProcessedMaterials > amount)
        {
            FinancialManager.AddFunds(ProcessedMaterialsPrice * amount);
            inventoryManager.RemoveProcessedMaterials(amount);
        }
    }

    public void SellAllMaterials()
    {
        FinancialManager.AddFunds(ProcessedMaterialsPrice * inventoryManager.ProcessedMaterials);
        inventoryManager.RemoveProcessedMaterials(inventoryManager.ProcessedMaterials);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager FinancialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private float ProcessedMaterialsPrice = 26.37f;

    public void SellProcessedMaterials()
    {
        if (inventoryManager.ProcessedMaterials > 0)
        {
            inventoryManager.RemoveProcessedMaterials(1);
            FinancialManager.AddFunds(ProcessedMaterialsPrice);
        }
    }
}

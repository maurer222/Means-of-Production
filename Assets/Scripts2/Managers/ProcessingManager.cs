using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessingManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private float ProcessingCost = 0.21f;

    public List<GameObject> machines;

    public void ProcessMaterials(int amount)
    {
        if(inventoryManager.RawMaterials >= amount && financialManager.Funds > (ProcessingCost * amount))
        {
            financialManager.RemoveFunds(ProcessingCost * amount);
            inventoryManager.RemoveRawMaterials(amount);
            inventoryManager.AddProcessedMaterials(amount);
        }
    }

    public void ProcessAllMaterials()
    {
        financialManager.RemoveFunds(ProcessingCost * inventoryManager.RawMaterials);
        inventoryManager.AddProcessedMaterials(inventoryManager.RawMaterials);
        inventoryManager.RemoveRawMaterials(inventoryManager.RawMaterials);
    }
}

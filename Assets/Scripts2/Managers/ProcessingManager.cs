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

    public void ProcessMaterials()
    {
        if(inventoryManager.RawMaterials > 0 && financialManager.Funds > ProcessingCost)
        {
            inventoryManager.RemoveRawMaterials(1);
            inventoryManager.AddProcessedMaterials(1);
            financialManager.RemoveFunds(ProcessingCost);
        }
    }
}

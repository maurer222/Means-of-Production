using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SalesManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager FinancialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private float ProcessedMaterialsPrice = 26.37f;
    [SerializeField] private TMP_InputField salesInput;

    public void SellProcessedMaterials()
    {
        if (inventoryManager.ProcessedMaterials > int.Parse(salesInput.text))
        {
            FinancialManager.AddFunds(ProcessedMaterialsPrice * int.Parse(salesInput.text));
            inventoryManager.RemoveProcessedMaterials(int.Parse(salesInput.text));
        }
    }

    public void SellAllMaterials()
    {
        FinancialManager.AddFunds(ProcessedMaterialsPrice * inventoryManager.ProcessedMaterials);
        inventoryManager.RemoveProcessedMaterials(inventoryManager.ProcessedMaterials);
    }
}

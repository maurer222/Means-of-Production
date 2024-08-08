using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PurchasingManager : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private FinancialManager financialManager;
    [SerializeField] private EmployeeManager employeeManager;
    [SerializeField] private TaskManager taskManager;
    [SerializeField] private Transform receivingArea;
    [SerializeField] private float RawMaterialCost = 18.21f;
    [SerializeField] private TMP_InputField purchasingInput;

    public void CreateSelectedPurchaseOrder()
    {
        int amount = int.Parse(purchasingInput.text);
        int numShipments = Mathf.CeilToInt((float)amount / 20);

        int numPallets = amount;
        GenerateUnloadingTask(numPallets);
    }

    public void CreateMaxPurchaseOrder()
    {
        int amount = (int)(financialManager.Funds / RawMaterialCost);
        int numShipments = Mathf.CeilToInt((float)amount / 20);

        int numPallets = amount;
        GenerateUnloadingTask(numPallets);
    }

    private void GenerateUnloadingTask(int numPallets)
    {
        var goals = new List<Goal>
        {
            new Goal("UnloadTruck",
                new Dictionary<string, bool> { { "TruckArrived", true } },
                new Dictionary<string, bool> { { "TruckUnloaded", true } }),

            new Goal("PutAwayMaterials",
                new Dictionary<string, bool> { { "ItemsInReceiving", true } },
                new Dictionary<string, bool> { { "ItemsPutAway", true } })
        };

        var actions = new List<Action>
        {
            new Action("ReserveForklift",
                new Dictionary<string, bool> { { "EquipmentAvailable", true } },
                new Dictionary<string, bool> { { "ForkliftReserved", true } },
                1.0f, employee => employee.ReserveForklift()),

            new Action("MoveToTruck",
                new Dictionary<string, bool> { { "ForkliftReserved", true } },
                new Dictionary<string, bool> { { "AtTruck", true } },
                1.0f, employee => employee.MoveToTruck()),

            new Action("PickUpPallet",
                new Dictionary<string, bool> { { "AtTruck", true } },
                new Dictionary<string, bool> { { "PalletPicked", true } },
                1.0f, employee => employee.PickUpPallet()),

            new Action("MoveToReceiving",
                new Dictionary<string, bool> { { "PalletPicked", true } },
                new Dictionary<string, bool> { { "AtReceiving", true } },
                1.0f, employee => employee.MoveToReceiving()),

            new Action("PlaceInReceiving",
                new Dictionary<string, bool> { { "AtReceiving", true } },
                new Dictionary<string, bool> { { "PalletInReceiving", true } },
                1.0f, employee => employee.PlaceInReceiving())
        };

        Task unloadingTask = new Task(numPallets, goals, actions);
        unloadingTask.Priority = 10;
        taskManager.AddTask(unloadingTask);
    }   
}

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

        //for (int i = 0; i < numShipments; i++)
        //{
        //    int materialsToPurchase = Mathf.Min(amount, 20);
        //    if (financialManager.Funds >= materialsToPurchase * RawMaterialCost)
        //    {
        //        financialManager.RemoveFunds(materialsToPurchase * RawMaterialCost);
        //        for (int j = 0; j < materialsToPurchase; j++)
        //        {
        //            //Task moveToReceiving = new Task(Task.TaskType.UnlockTruck, 1, 10f, 90);
        //            //Task moveToStorage = new Task(Task.TaskType.PutawayFromReceiving, 1, 10f, 80);
        //            var goalUnloadTruck = new Goal("UnloadTruck",
        //                new Dictionary<string, bool> { { "TruckArrived", true } },
        //                new Dictionary<string, bool> { { "TruckUnloaded", true } });

        //            var goalPutAwayMaterials = new Goal("PutAwayMaterials",
        //                new Dictionary<string, bool> { { "ItemsInReceiving", true } },
        //                new Dictionary<string, bool> { { "ItemsPutAway", true } });
        //            taskManager.AddTask(moveToReceiving);
        //            taskManager.AddTask(moveToStorage);
        //            inventoryManager.AddRawMaterials(1);
        //        }
        //        amount -= materialsToPurchase;
        //    }
        //    else
        //    {
        //        Debug.Log("Not enough funds to complete the purchase order!");
        //        break;
        //    }
        //}


        //for (int i = 0; i < numShipments; i++)
        //{
        //    int materialsToPurchase = Mathf.Min(amount, 20);
        //    if (financialManager.Funds >= materialsToPurchase * RawMaterialCost)
        //    {
        //        financialManager.RemoveFunds(materialsToPurchase * RawMaterialCost);
        //        for (int j = 0; j < materialsToPurchase; j++)
        //        {
        //            Task moveToReceiving = new Task(Task.TaskType.UnlockTruck, 1, 10f, 90);
        //            Task moveToStorage = new Task(Task.TaskType.PutawayFromReceiving, 1, 10f, 80);
        //            taskManager.AddTask(moveToReceiving);
        //            taskManager.AddTask(moveToStorage);
        //            inventoryManager.AddRawMaterials(1);
        //        }
        //        amount -= materialsToPurchase;
        //    }
        //    else
        //    {
        //        Debug.Log("Not enough funds to complete the purchase order!");
        //        break;
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
        //

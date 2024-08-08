using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public DayCycleManager dayCycleManager;

    [SerializeField] public List<Transform> receivingDocks;
    [SerializeField] public List<Transform> loadingDocks;

    [SerializeField] public List<StorageArea> ReceivingAreas { get; private set; }
    [SerializeField] public List<StorageArea> GeneralStorageAreas { get; private set; }
    [SerializeField] public List<StorageArea> ShippingAreas { get; private set; }

    public int RawMaterials = 0;
    public int ProcessedMaterials = 0;

    public event Action<int> OnRawMaterialsChanged;
    public event Action<int> OnProcessedMaterialsChanged;

    private void Awake()
    {
        ReceivingAreas = new List<StorageArea>();
        GeneralStorageAreas = new List<StorageArea>();
        ShippingAreas = new List<StorageArea>();
    }

    public bool AddMaterialsToStorage(StorageArea.StorageType type, int amount)
    {
        var storageAreas = GetStorageAreasByType(type);
        foreach (var storageArea in storageAreas)
        {
            if (storageArea.AddMaterials(amount))
            {
                return true;
            }
        }
        return false;
    }

    public bool RemoveMaterialsFromStorage(StorageArea.StorageType type, int amount)
    {
        var storageAreas = GetStorageAreasByType(type);
        foreach (var storageArea in storageAreas)
        {
            if (storageArea.RemoveMaterials(amount))
            {
                return true;
            }
        }
        return false;
    }

    public void RegisterStorageArea(StorageArea storageArea)
    {
        switch (storageArea.Type)
        {
            case StorageArea.StorageType.Receiving:
                ReceivingAreas.Add(storageArea);
                break;
            case StorageArea.StorageType.General:
                GeneralStorageAreas.Add(storageArea);
                break;
            case StorageArea.StorageType.Shipping:
                ShippingAreas.Add(storageArea);
                break;
        }
    }

    public void DeregisterStorageArea(StorageArea storageArea)
    {
        switch (storageArea.Type)
        {
            case StorageArea.StorageType.Receiving:
                ReceivingAreas.Remove(storageArea);
                break;
            case StorageArea.StorageType.General:
                GeneralStorageAreas.Remove(storageArea);
                break;
            case StorageArea.StorageType.Shipping:
                ShippingAreas.Remove(storageArea);
                break;
        }
    }

    public int GetTotalAvailableCapacity(StorageArea.StorageType type)
    {
        var storageAreas = GetStorageAreasByType(type);
        int totalAvailableCapacity = 0;
        foreach (var storageArea in storageAreas)
        {
            totalAvailableCapacity += storageArea.AvailableCapacity();
        }
        return totalAvailableCapacity;
    }

    private List<StorageArea> GetStorageAreasByType(StorageArea.StorageType type)
    {
        switch (type)
        {
            case StorageArea.StorageType.Receiving:
                return ReceivingAreas;
            case StorageArea.StorageType.General:
                return GeneralStorageAreas;
            case StorageArea.StorageType.Shipping:
                return ShippingAreas;
            default:
                return null;
        }
    }

    public void AddRawMaterials(int amount)
    {
        RawMaterials += amount;
        OnRawMaterialsChanged?.Invoke(RawMaterials);
    }

    public void RemoveRawMaterials(int amount)
    {
        RawMaterials-= amount;
        OnRawMaterialsChanged?.Invoke(RawMaterials);
    }

    public void AddProcessedMaterials(int amount)
    {
        ProcessedMaterials += amount;
        dayCycleManager.ProcessMaterials(amount);
        OnProcessedMaterialsChanged?.Invoke(ProcessedMaterials);
    }

    public void RemoveProcessedMaterials(int amount)
    {
        ProcessedMaterials -= amount;
        OnProcessedMaterialsChanged?.Invoke(ProcessedMaterials);
    }
}

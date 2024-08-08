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

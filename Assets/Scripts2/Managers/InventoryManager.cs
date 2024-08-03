using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] public List<Transform> receivingAreas;
    [SerializeField] public List<Transform> loadingAreas;
    [SerializeField] public List<Transform> storageAreas;
    [SerializeField] public List<Transform> receivingDocks;
    [SerializeField] public List<Transform> loadingDocks;
    [SerializeField] public DayCycleManager dayCycleManager;
    public int RawMaterials = 0;
    public int ProcessedMaterials = 0;

    public event Action<int> OnRawMaterialsChanged;
    public event Action<int> OnProcessedMaterialsChanged;

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

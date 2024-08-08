using UnityEngine;

public class StorageArea : MonoBehaviour
{
    public enum StorageType { Receiving, General, Shipping }

    public StorageType Type;
    public int MaxCapacity;
    public int UsedCapacity { get; private set; }

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.RegisterStorageArea(this);
        }
    }

    public bool AddMaterials(int amount)
    {
        if (UsedCapacity + amount <= MaxCapacity)
        {
            UsedCapacity += amount;
            return true;
        }
        return false;
    }

    public bool RemoveMaterials(int amount)
    {
        if (UsedCapacity >= amount)
        {
            UsedCapacity -= amount;
            return true;
        }
        return false;
    }

    public int AvailableCapacity()
    {
        return MaxCapacity - UsedCapacity;
    }

    private void OnDestroy()
    {
        if (inventoryManager != null)
        {
            inventoryManager.DeregisterStorageArea(this);
        }
    }
}

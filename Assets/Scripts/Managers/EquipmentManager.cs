using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance { get; private set; }

    private List<Equipment> allEquipment = new List<Equipment>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        allEquipment.Add(new Forklift("Forklift1", this.transform, 1));
    }

    public void RegisterEquipment(Equipment equipment)
    {
        allEquipment.Add(equipment);
    }

    public Equipment GetAvailableEquipment()
    {
        // Prioritize equipment based on the type
        foreach (var equipment in allEquipment.OrderBy(e => GetEquipmentPriority(e)))
        {
            if (equipment.IsAvailable)
            {
                equipment.Reserve();
                return equipment;
            }
        }
        return null;
    }

    public Equipment GetAvailableEquipment<T>() where T : Equipment
    {
        foreach (var equipment in allEquipment)
        {
            if (equipment is T && equipment.IsAvailable)
            {
                equipment.Reserve();
                return equipment;
            }
        }
        return null;
    }

    private int GetEquipmentPriority(Equipment equipment)
    {
        if (equipment is Forklift) return 50;
        if (equipment is PalletJack) return 100;
        return int.MaxValue;
    }

    public void ReleaseEquipment(Equipment equipment)
    {
        equipment.Release();
    }
}
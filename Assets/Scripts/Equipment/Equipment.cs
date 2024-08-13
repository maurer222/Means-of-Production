using UnityEngine;

public class Equipment
{
    public string EquipmentName { get; private set; }
    public bool IsAvailable { get; private set; }
    public Transform Location { get; private set; }

    public int palletMax;
    public int palletCurrent;

    public Equipment(string name, Transform location, int palletMax, int palletCurrent = 0)
    {
        EquipmentName = name;
        IsAvailable = true;
        Location = location;
        this.palletMax = palletMax;
        this.palletCurrent = palletCurrent;
    }

    public virtual void Reserve()
    {
        IsAvailable = false;
    }

    public virtual void Release()
    {
        IsAvailable = true;
    }
}
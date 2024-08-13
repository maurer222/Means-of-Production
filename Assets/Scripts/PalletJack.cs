using UnityEngine;

public class PalletJack : Equipment
{
    public PalletJack(string name, Transform location, int palletMax) : base(name, location, palletMax)
    {
    }

    // Override methods if needed, or add specific properties/methods for PalletJack
    public override void Reserve()
    {
        base.Reserve();
        // Add any additional logic specific to PalletJack reservation
        Debug.Log("Pallet Jack reserved.");
    }

    public override void Release()
    {
        base.Release();
        // Add any additional logic specific to PalletJack release
        Debug.Log("Pallet Jack released.");
    }
}
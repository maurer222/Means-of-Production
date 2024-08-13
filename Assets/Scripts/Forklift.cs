using UnityEngine;

public class Forklift : Equipment
{
    public Forklift(string name, Transform location, int palletMax) : base(name, location, palletMax)
    {
    }

    // Override methods if needed, or add specific properties/methods for Forklift
    public override void Reserve()
    {
        base.Reserve();
        // Add any additional logic specific to Forklift reservation
        Debug.Log("Forklift reserved.");
    }

    public override void Release()
    {
        base.Release();
        // Add any additional logic specific to Forklift release
        Debug.Log("Forklift released.");
    }
}
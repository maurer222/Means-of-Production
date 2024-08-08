using UnityEngine;

public class ComponentMaterial : Material
{
    public ComponentMaterial(string name, string description, float weight, float value)
    {
        Name = name;
        Description = description;
        Weight = weight;
        Value = value;
    }
}

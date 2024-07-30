using System.Collections.Generic;
using UnityEngine;

public class ReceivingInventory : MonoBehaviour
{
    [SerializeField] List<RawInventoryItem> Inventory = new List<RawInventoryItem>();
    [SerializeField] private int maxInventory;
    [SerializeField] private GameObject rawMaterialObject;
    //[SerializeField] private float palletSize = 1.0f; // Size of the pallet (assuming 1x1)
    [SerializeField] private float spacing = 0.025f; // Spacing between pallets

    [SerializeField] private Renderer planeRenderer;
    [SerializeField] private float planeWidth;
    [SerializeField] private float planeHeight;
    [SerializeField] private int rows;
    [SerializeField] private int columns;

    private void Start()
    {
        planeRenderer = gameObject.GetComponent<Renderer>();
        planeWidth = planeRenderer.bounds.size.x;
        planeHeight = planeRenderer.bounds.size.z; // Use 'z' for height since Unity's plane is on the XZ plane

        // Calculate the number of rows and columns based on pallet size and spacing
        rows = Mathf.FloorToInt(planeHeight);
        columns = Mathf.FloorToInt(planeWidth);

        maxInventory = rows * columns;
        Inventory.Clear();
    }

    public int AddInventoryItems(int itemsCount, RawInventoryItem item)
    {
        int itemsAdded = 0;

        for (int i = Inventory.Count; i < maxInventory && itemsAdded < itemsCount; i++)
        {
            // Calculate the position of the new item
            int row = i / columns;
            int column = i % columns;

            // Calculate the spawn position
            float xPos = gameObject.transform.position.x - planeWidth / 2 + spacing + column;
            float zPos = gameObject.transform.position.z - planeHeight / 2 + spacing + row;

            Vector3 spawnPosition = new Vector3(xPos, gameObject.transform.position.y, zPos);

            // Instantiate the pallet
            Instantiate(rawMaterialObject, spawnPosition, Quaternion.identity);
            Inventory.Add(item);
            itemsAdded++;

            Debug.Log($"Item {Inventory.Count} added at position ({xPos}, {zPos})");
        }

        return itemsAdded;
    }
}
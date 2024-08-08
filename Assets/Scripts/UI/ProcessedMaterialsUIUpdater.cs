using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProcessedMaterialsUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI processedText;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        UpdateRawText(inventoryManager.RawMaterials);
        inventoryManager.OnProcessedMaterialsChanged += UpdateRawText;
    }

    private void OnDestroy()
    {
        inventoryManager.OnProcessedMaterialsChanged -= UpdateRawText;
    }

    private void UpdateRawText(int newRaw)
    {
        processedText.text = "Processed: " + newRaw.ToString();
    }
}

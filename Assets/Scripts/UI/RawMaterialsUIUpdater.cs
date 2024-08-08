using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RawMaterialsUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI rawText;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>();
        UpdateRawText(inventoryManager.RawMaterials);
        inventoryManager.OnRawMaterialsChanged += UpdateRawText;
    }

    private void OnDestroy()
    {
        inventoryManager.OnRawMaterialsChanged -= UpdateRawText;
    }

    private void UpdateRawText(int newRaw)
    {
        rawText.text = "Raw: " + newRaw.ToString();
    }
}

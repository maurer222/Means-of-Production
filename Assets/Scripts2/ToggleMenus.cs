using UnityEngine;
using UnityEngine.UI;

public class ToggleMenus : MonoBehaviour
{
    public void ToggleSubPanel(GameObject subPanel)
    {
        // Toggle the active state of the sub-panel
        subPanel.SetActive(!subPanel.activeSelf);
    }
}
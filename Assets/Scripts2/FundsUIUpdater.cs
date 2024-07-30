using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FundsUIUpdater : MonoBehaviour
{
    public TextMeshProUGUI fundsText;
    private FinancialManager financialManager;

    private void Start()
    {
        financialManager = FindObjectOfType<FinancialManager>();
        UpdateFundsText(financialManager.Funds);
        financialManager.OnFundsChanged += UpdateFundsText;
    }

    private void OnDestroy()
    {
        financialManager.OnFundsChanged -= UpdateFundsText;
    }

    private void UpdateFundsText(float newFunds)
    {
        fundsText.text = "Funds: $" + newFunds.ToString("F2");
    }
}
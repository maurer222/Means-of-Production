using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DayCycleManager : MonoBehaviour
{
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI dailySummaryText;
    public float dayDuration = 60f;
    private int currentDay = 1;
    private float dailyTime = 0f;
    private float dailyEarnings = 0f;
    private int materialsProcessed = 0;

    void Start()
    {
        StartDay();
    }

    void Update()
    {
        dailyTime += Time.deltaTime;
        dayText.text = "Day: " + currentDay + " | " + (dayDuration - (int)dailyTime);

        if (dailyTime >= dayDuration)
        {
            EndDay();
        }
    }

    void StartDay()
    {
        dailyTime = 0f;
        dailyEarnings = 0f;
        materialsProcessed = 0;
        dayText.text = "Day: " + currentDay + " | " + (dayDuration - (int)dailyTime);
    }

    void EndDay()
    {
        dailySummaryText.text = "Day " + currentDay + " Summary:\n" +
                                "Earnings: $" + dailyEarnings.ToString("F2") + "\n" +
                                "Materials Processed: " + materialsProcessed;

        currentDay++;
        StartDay();
    }

    // Example methods to update earnings and processed materials
    public void AddEarnings(float amount)
    {
        dailyEarnings += amount;
    }

    public void RemoveEarnings(float amount)
    {
        dailyEarnings -= amount;
    }

    public void ProcessMaterials(int amount)
    {
        materialsProcessed += amount;
    }
}
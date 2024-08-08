using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinancialManager : MonoBehaviour
{
    public float Funds = 100.00f;
    public DayCycleManager dayCycleManager;

    public event Action<float> OnFundsChanged;

    public void AddFunds(float amount)
    {
        Funds += amount;
        dayCycleManager.AddEarnings(amount);
        OnFundsChanged?.Invoke(Funds);
    }

    public void RemoveFunds(float amount)
    {
        if (Funds > amount)
        {
            Funds -= amount;
            dayCycleManager.RemoveEarnings(amount);
            OnFundsChanged?.Invoke(Funds);
        }
    }
}

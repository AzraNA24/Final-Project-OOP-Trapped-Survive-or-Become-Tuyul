using UnityEngine;


public class CurrencyManager : MonoBehaviour
{
    public int TotalMoney = 100;

    public void AddMoney(int amount)
    {
        if (amount > 0)
        {
            TotalMoney += amount;
            Debug.Log($"Uang ditambahkan sebesar {amount}. Total uang: {TotalMoney}");
        }
    }

    public bool DeductMoney(int amount)
    {
        if (TotalMoney >= amount)
        {
            TotalMoney -= amount;
            Debug.Log($"Uang dikurangi sebesar {amount}. Sisa uang: {TotalMoney}");
            return true;
        }
        else
        {
            Debug.Log("Uang tidak cukup.");
            return false;
        }
    }

    public void ShowBalance()
    {
        Debug.Log($"Saldo saat ini: {TotalMoney}");
    }
}
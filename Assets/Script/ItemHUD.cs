using UnityEngine;
using TMPro;

public class ItemHUD : MonoBehaviour
{
    public TextMeshProUGUI moneyText; // UI untuk Money
    public TextMeshProUGUI potionsText; // UI untuk Potions
    public Player player; // Referensi ke Player

    void Start()
    {
        UpdateMoneyUI();
        UpdatePotionsUI();
    }

    public void UpdateMoneyUI()
    {
        if (player.CurrencyManager != null)
        {
            moneyText.text = $"Money: {player.CurrencyManager.TotalMoney}";
        }
    }

    public void UpdatePotionsUI()
    {
        potionsText.text = $"Potions: {player.potions}";
    }
}

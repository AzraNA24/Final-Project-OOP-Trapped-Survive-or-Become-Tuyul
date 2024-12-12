using UnityEngine;

public class MoneyBag : Item
{
    public int Amount;

    private void Awake()
    {
        Name = "Money Bag";
    }

    public override void Use(GameObject character)
    {
        var player = character.GetComponent<Player>();
        if (player != null)
        {
            player.CurrencyManager.AddMoney(Amount);
            Debug.Log($"{Name} memberikan uang sebesar {Amount} kepada {player.Name}.");
            Amount = 0; // Setelah dipindahkan, uang habis
        }
        else
        {
            Debug.LogWarning("Player tidak ditemukan!");
        }
    }
}
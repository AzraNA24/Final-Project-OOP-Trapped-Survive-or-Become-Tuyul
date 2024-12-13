using System.Collections;
using UnityEngine;

public class MrRizzler : Tuyul
{
    public MrRizzler()
    {
        Name = "Mr. Rizzler; The Charmer of Chaos";
        maxHealth = 50;
        AttackPower = 10;
        Money = 30;
    }

    public override bool TakeDamage(int damage, Player playerCharacter)
    {
        currentHealth -= damage;
        Debug.Log($"{Name} menerima {damage} damage! Sisa HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true; 
        }

        if (random.NextDouble() < 0.4)
        {
            int stolenAmount = random.Next(1, 101);
            if (playerCharacter.CurrencyManager.DeductMoney(stolenAmount))
            {
                TuyulAnim.SetTrigger("TPBP");
                Debug.Log($"{Name} menggunakan jurus rahasia: 'Tangan Panjang, Badan Pendek'. Kamu kehilangan uang sebesar {stolenAmount}!");
            }
        }

        // Special Skill: Seduce You To Death (20% chance)
        if (random.NextDouble() < 0.2)
        {
            UseSeduceYouToDeath(playerCharacter);
        }
        else
        {
            // basic attack
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    public void UseSeduceYouToDeath(Player playerCharacter)
    {
        TuyulAnim.SetTrigger("Ulti");

        // Mengurangi critical chance pemain (15%)
        playerCharacter.criticalChance -= 0.15f;
        if (playerCharacter.criticalChance < 0) playerCharacter.criticalChance = 0;

        // Mengurangi efektivitas health potion (50%)
        playerCharacter.healthPotionEffectiveness -= 0.5f;
        if (playerCharacter.healthPotionEffectiveness < 0.1f) playerCharacter.healthPotionEffectiveness = 0.1f;

        Debug.Log($"{Name} menggunakan jurus spesial 'Seduce You To Death'! Efek health potion pemain berkurang dan critical chance turun menjadi {playerCharacter.criticalChance * 100}%.");
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }
}
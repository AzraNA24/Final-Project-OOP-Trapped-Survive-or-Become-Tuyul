using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuyul : MonoBehaviour
{
    public Animator TuyulAnim;
    public string Name = "Tuyul; Scurry Impish Little Trickster";
    public int maxHealth = 50;
    public int currentHealth;
    public int AttackPower = 15;
    public int Money = 30;
    public System.Random random = new System.Random();

    void Start()
    {
        currentHealth = maxHealth;
        TuyulAnim.SetBool("TurnBased", true);
    }

    // Method to handle taking damage with additional Tuyul abilities
    public virtual bool TakeDamage(int damage, Player playerCharacter)
    {
        // Retaliation damage if the attack is 0
        if (damage == 0)
        {
            int retaliationDamage = 7;
            playerCharacter.TakeDamage(retaliationDamage);
            Debug.Log($"{playerCharacter.Name} menerima {retaliationDamage} damage dari serangan balik! Sisa HP: {playerCharacter.currentHealth}");
            return false;
        }

        currentHealth -= damage;
        Debug.Log($"{Name} menerima {damage} damage! Sisa HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true; // Tuyul is dead
        }

        playerCharacter.TakeDamage(AttackPower);
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu'. {playerCharacter.Name} menerima {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");

        // 40% chance to steal money
        if (random.NextDouble() < 0.4)
        {
            int stolenAmount = random.Next(1, 101);
            if (playerCharacter.CurrencyManager.DeductMoney(stolenAmount))
            {
                TuyulAnim.SetTrigger("TPBP");
                Debug.Log($"{Name} menggunakan jurus rahasia: 'Tangan Panjang, Badan Pendek'. Kamu kehilangan uang sebesar {stolenAmount}!");
            }
        }

        // Offer to surrender if health is low
        if (currentHealth <= 15)
        {
            Debug.Log($"{Name} menawarkan uang sebesar {Money} untuk ganti nyawanya. Terima? (1 = Iya, 2 = Tidak)");

            // Simulate player choice (modify as needed for your game input)
            int playerChoice = 1; // Example choice, replace with actual input handling

            if (playerChoice == 1)
            {
                playerCharacter.CurrencyManager.AddMoney(Money);
                Debug.Log($"{Name} melarikan diri!");
                currentHealth = 0;
                return true;
            }
        }

        return false;
    }
}

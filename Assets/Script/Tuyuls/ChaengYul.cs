using System.Collections;
using UnityEngine;

public class ChaengYul : Tuyul
{
    public ChaengYul()
    {
        Name = "ChaengYul; Bestie of Pocong";
        maxHealth = 200;
        AttackPower = 15;
        Money = 100;
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

        // Passive Skill: Cursed Hop (30% chance)
        if (random.NextDouble() < 0.3)
        {
            int healAmount = Mathf.RoundToInt(maxHealth * 0.15f); // Heal 15% dari max HP
            currentHealth += healAmount;

            Debug.Log($"{Name} menggunakan 'Cursed Hop' dan memulihkan {healAmount} HP! Sisa HP: {currentHealth}");
        }

        // Special Skill: Beyond the Grave (20% chance)
        if (random.NextDouble() < 0.2)
        {
            UseBeyondTheGrave(playerCharacter);
        }
        else 
        {
            // basic attack
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    public void UseBeyondTheGrave(Player playerCharacter)
    {
        TuyulAnim.SetTrigger("Ulti");
        int Ultimate = AttackPower * 2;
        playerCharacter.TakeDamage(Ultimate);
        Debug.Log($"{Name} menggunakan jurus spesial 'Beyond the Grave'! {playerCharacter.Name} menerima {Ultimate} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }
}
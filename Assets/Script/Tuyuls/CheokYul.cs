using System.Collections;
using UnityEngine;

public class CheokYul : Tuyul
{
    private bool isFlying = false; // Status untuk passive skill
    private int poisonDuration;
    private bool isPoisonActive = false;

    public CheokYul()
    {
        Name = "CheokYul; Impish Who Studies under The Ancient Beast [Kecoak Terbang]";
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

        // Passive Talent: The Flying Horror
        if (currentHealth <= maxHealth / 2 && !isFlying)
        {
            isFlying = true;
            // tambahin kode buat animasi dia terbang
            // terus ini efeknya apa ya kalo dia terbang? ga bisa diserang jarak deket?

            Debug.Log($"{Name} masuk ke mode 'The Flying Horror'!");
        }
     
        if (Random.value < 0.2f) // Special Skill: The Democracy
        {
            UseTheDemocracy(playerCharacter);
        }
        else if (Random.value < 0.2f) // Special Skill: Monster Lurks Beneath The Shadow of The Dawn
        {
            UsePoison(playerCharacter);
        }
        else
        {
            // basic attack
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    private void UseTheDemocracy(Player playerCharacter)
    {
        int roachesCount = Random.Range(2, 4); // Memanggil 2-3 kecoak kecil
        Debug.Log($"{Name} memanggil {roachesCount} kecoak kecil untuk menyerang!");

        for (int i = 0; i < roachesCount; i++)
        {
            int roachDamage = Random.Range(5, 10); // Damage tiap kecoak
            playerCharacter.TakeDamage(roachDamage);
            Debug.Log($"Seekor kecoak menyerang dan memberikan {roachDamage} damage! Sisa HP pemain: {playerCharacter.currentHealth}");
        }
    }

    public void UsePoison(Player playerCharacter)
    {
        poisonDuration = 3; // Atur durasi poison
        isPoisonActive = true; // Aktifkan poison
        Debug.Log($"{Name} menggunakan jurus 'Monster Lurks Beneath The Shadow of The Dawn'! Pemain terkena efek poison selama {poisonDuration} giliran.");
    }

    public void ApplyPoison(Player playerCharacter)
    {
        if (isPoisonActive && poisonDuration > 0)
        {
            int poisonDamage = 10; // Damage per giliran
            playerCharacter.TakeDamage(poisonDamage);
            poisonDuration--; 

            Debug.Log($"Poison effect: Pemain menerima {poisonDamage} damage. Sisa HP: {playerCharacter.currentHealth}. Sisa giliran poison: {poisonDuration}");

            if (poisonDuration == 0)
            {
                isPoisonActive = false; 
                Debug.Log("Poison effect selesai!");
            }
        }
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }
}
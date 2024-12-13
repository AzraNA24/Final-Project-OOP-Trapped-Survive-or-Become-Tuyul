using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Aventurine : Tuyul
{
    public Aventurine()
    {
        Name = "Aventurine; The Sparkling Trickster";
        maxHealth = 50;
        AttackPower = 15;
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
        // 40% chance to use "The Great Gatsby" instead of normal attack
        if (random.NextDouble() < 0.3)
        {
            UseTheGreatGatsby(playerCharacter);
        }
        else
        {
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    private void UseTheGreatGatsby(Player playerCharacter)
    {
        // int stolenAmount = random.Next(30, 101); // Steals a larger sum
        // if (playerCharacter.CurrencyManager.DeductMoney(stolenAmount))
        // {
        //     if (TuyulAnim != null)
        //     {
        //         TuyulAnim.SetTrigger("Ulti");
        //     }
        //     else
        //     {
        //         Debug.LogError("Animator pada Tuyul tidak diatur!");
        //     }
        //     Debug.Log($"{Name} menggunakan jurus spesial 'The Great Gatsby' dan mencuri uangmu sebesar {stolenAmount}!");
        // }

        TuyulAnim.SetTrigger("Ulti");
        int Ultimate = AttackPower + AttackPower/2;
        playerCharacter.TakeDamage(Ultimate);
        Debug.Log($"{Name} memberikan {AttackPower*2} damage tambahan dengan jurus 'The Great Gatsby'! Sisa HP: {playerCharacter.currentHealth}");
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

}

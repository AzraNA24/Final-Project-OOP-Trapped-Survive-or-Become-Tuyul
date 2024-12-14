using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Aventurine : Tuyul
{
    private bool canFUA = false;
    public Aventurine()
    {
        Name = "Aventurine; The Sparkling Trickster";
        maxHealth = 50;
        AttackPower = 10;
        Money = 30;
        Type = TuyulType.Aventurine;
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
    return false;
}
    public override void EnemyAction(Player playerCharacter)
    {
        if (random.NextDouble() < 0.4)
        {
            int stolenAmount = random.Next(1, 101);
            if (playerCharacter.CurrencyManager.DeductMoney(stolenAmount))
            {
                TuyulAnim.SetTrigger("TPBP");
                Debug.Log($"{Name} menggunakan jurus rahasia: 'Tangan Panjang, Badan Pendek'. Kamu kehilangan uang sebesar {stolenAmount}!");
            }
        }
        if (random.NextDouble() < 0.4)
            UseTheGreatGatsby(playerCharacter);
        else
            NormalAttack(playerCharacter);
    }
    public void UseTheGreatGatsby(Player playerCharacter)
    {
        TuyulAnim.SetTrigger("Ulti");
        int Ultimate = AttackPower + AttackPower/2;
        playerCharacter.TakeDamage(Ultimate);
        Debug.Log($"{Name} memberikan {AttackPower*1.5} damage tambahan dengan jurus 'The Great Gatsby'! Sisa HP: {playerCharacter.currentHealth}");
        canFUA = true;
    }

    public override void NormalAttack(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

    public void FUA (Player playerCharacter, bool playerCrit)
    {
        if (canFUA && playerCrit){
            TuyulAnim.SetTrigger("Ulti");
            int FUA = AttackPower/2;
            playerCharacter.TakeDamage(FUA);
            Debug.Log($"{Name} melakukan Follow-Up Attack dan memberikan {FUA} damage tambahan! Sisa HP: {playerCharacter.currentHealth}");
        }
    }

}

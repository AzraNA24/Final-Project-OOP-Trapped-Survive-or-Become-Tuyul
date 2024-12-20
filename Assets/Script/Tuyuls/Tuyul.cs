using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuyul : MonoBehaviour
{
    public Animator TuyulAnim;
    public string Name = "Tuyul; Scurry Impish Little Trickster";
    public int maxHealth;
    public int currentHealth;
    public int AttackPower;
    public int Money;
    public System.Random random = new System.Random();
    public TuyulType Type { get; set; }

    void Start()
    {
        currentHealth = maxHealth;
        TuyulAnim.SetBool("TurnBased", false);

        if (TuyulAnim == null)
        {
            TuyulAnim = GetComponent<Animator>(); // Ambil komponen Animator
            if (TuyulAnim == null)
            {
                Debug.LogError($"{name} tidak memiliki komponen Animator!");
            }
        }
    }

    // Method to handle taking damage with additional Tuyul abilities
    public virtual bool TakeDamage(int damage, Player playerCharacter)
    {
        currentHealth -= damage;
        Debug.Log($"{Name} menerima {damage} damage! Sisa HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true; // Tuyul is dead
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

    public enum TuyulType
    {
    Aventurine,
    MrRizzler,
    RollyPolly,
    ChaengYul,
    CheokYul,
    JaekYul
    }
    public virtual void EnemyAction(Player playerCharacter)
    {
        NormalAttack(playerCharacter);
    }
    public virtual void NormalAttack(Player playerCharacter)
{
    playerCharacter.TakeDamage(AttackPower);
    TuyulAnim.SetTrigger("Throws");
    Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
}

}

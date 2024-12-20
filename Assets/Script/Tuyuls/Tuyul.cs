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
    private bool isOfferingMoney = false;
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
        if (currentHealth <= maxHealth * 0.3f && !isOfferingMoney)
        {
            isOfferingMoney = true;
            Debug.Log($"{Name} menawarkan uang sebesar {Money} untuk ganti nyawanya. Terima? (1 = Iya, 2 = Tidak)");

            StartCoroutine(WaitForPlayerChoice(playerCharacter)); // Tunggu input pemain
            return false; 
        }

        return false;
    }

    private IEnumerator WaitForPlayerChoice(Player playerCharacter)
    {
        bool decisionMade = false;
        while (!decisionMade)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) // Pemain memilih "Terima"
            {
                Debug.Log($"{Name} melarikan diri setelah memberikan uang sebesar {Money}!");
                playerCharacter.CurrencyManager.AddMoney(Money);
                currentHealth = 0; // Tuyul mati
                decisionMade = true;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2)) // Pemain memilih "Tolak"
            {
                Debug.Log($"{Name} terus melawan!");
                decisionMade = true;
            }

            yield return null; // Tunggu frame berikutnya
        }

        isOfferingMoney = false; // Reset flag
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

    public enum TuyulType
    {
        Aventurine,
        MrRizzler,
        RollyPolly,
        ChaengYul,
        CheokYul,
        JaekYul
    }
}
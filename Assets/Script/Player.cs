using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int Health = 100;
    public int currentHealth;
    public CurrencyManager CurrencyManager = new CurrencyManager();
    private int bullets = 5;

    public float criticalChance = 0.3f; // Default 30%         eh ini blm ada implementasinya yak di serangan
    public float healthPotionEffectiveness = 1.0f; // Default 100%

    void Start()
    {
        currentHealth = Health;
        CurrencyManager.TotalMoney = 100;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            return true;
        }
        return false;
    }

    public void Heal(int amount)
    {
        currentHealth += Mathf.RoundToInt(amount * healthPotionEffectiveness);
        if (currentHealth > 100)
            currentHealth = 100;
    }

    public bool HasBullets()
    {
        if (bullets > 0)
        {
            bullets--;
            return true;
        }
        return false;
    }

    public void AddBullets(int amount)
    {
        bullets += amount;
    }
}

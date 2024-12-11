using UnityEngine;

public class Player : MonoBehaviour
{
    public string Name;
    public int Health = 100;
    public int currentHealth;
    public CurrencyManager CurrencyManager = new CurrencyManager();
    private int bullets = 5;

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
        currentHealth += amount;
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

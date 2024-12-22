using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public string Name;
    public int Health = 100;
    public int currentHealth;
    public CurrencyManager CurrencyManager;
    public int bullets = 5;
    public int potions = 1;
    public float criticalChance = 0.3f; // Default 30%         eh ini blm ada implementasinya yak di serangan re: udahh
    public float healthPotionEffectiveness = 1.0f; // Default 100%
    public ItemHUD itemHUD;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
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
        //if (currentHealth > 100)
        //    currentHealth = 100;
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
        Debug.Log($"Peluru ditambahkan sejumlah {amount}. Total peluru: {bullets}");
    }
    
    public bool HasPotion()
    {
        return potions > 0;
    }

    public bool UsePotion()
    {
        if (potions > 0)
        {
            potions--;
            Heal(20);
            Debug.Log($"Potion digunakan. Sisa potion: {potions}. Kesehatan saat ini: {currentHealth}");
            itemHUD.UpdatePotionsUI(); // Perbarui UI untuk potions
            return true;
        }
        else
        {
            Debug.Log("Tidak ada potion tersisa!");
            return false;
        }
    }

    public void AddPotion(int amount)
    {
        potions += amount;
        Debug.Log($"Potion ditambahkan sejumlah {amount}. Total potion: {potions}");
        itemHUD.UpdatePotionsUI(); // Perbarui UI untuk potions
    }
}

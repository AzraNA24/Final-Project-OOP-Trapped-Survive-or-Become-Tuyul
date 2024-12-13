using UnityEngine;

public class HealthPotion : Item
{
    public int HealthRestore { get; set; } = 20;
    private static int usageCount = 0;
    private const int MaxUsage = 5;

    private void Awake()
    {
        Name = "Health Potion";
    }

    public override void Use(GameObject character)
    {
        if (usageCount >= MaxUsage)
        {
            Debug.Log("Tidak bisa menggunakan potion lebih dari 5 kali selama pertarungan.");
            return;
        }

        public override void Use(GameObject character)
        {
            var player = character.GetComponent<Player>();
            if (player != null)
            {
                player.Heal(HealthRestore);
                Debug.Log($"{player.Name} menggunakan {Name} dan memulihkan {HealthRestore} HP. Sisa HP: {player.currentHealth}");
            }
            else
            {
                Debug.LogWarning("Player tidak ditemukan!");
            }
        }

        usageCount++;
    }

    public static void ResetUsageCount()
    {
        usageCount = 0;
    }
}
using UnityEngine;

public class Gun : Item
{
    public float DamageMultiplier = 0.15f; 

    private void Awake()
    {
        Name = "Gun";
    }

    public override void Use(GameObject character)
    {
        var player = character.GetComponent<Player>();
        if (player == null)
        {
            Debug.LogWarning("Player tidak ditemukan!");
            return;
        }

        if (!player.HasBullets())
        {
            Debug.Log($"{player.Name} mencoba menembak, tapi tidak memiliki peluru!");
            return;
        }

        var tuyul = FindObjectOfType<Tuyul>();
        if (tuyul == null)
        {
            Debug.LogWarning("Tidak ada Tuyul yang dapat diserang.");
            return;
        }

        // Hitung damage berdasarkan 15% dari uang pemain
        float damage = player.CurrencyManager.TotalMoney * DamageMultiplier;
        int roundedDamage = Mathf.RoundToInt(damage);

        tuyul.TakeDamage(roundedDamage, player);
        Debug.Log($"{player.Name} menyerang musuh dengan {damage} damage!");
    }
}
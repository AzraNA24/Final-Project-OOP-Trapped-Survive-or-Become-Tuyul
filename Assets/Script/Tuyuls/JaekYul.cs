using UnityEngine;

public class JaekYul : Tuyul
{
    private GameObject currentFormObject;

    public JaekYul()
    {
        Name = "Jaek Yul; Tuyul of All Trade, Master of All";
        maxHealth = 350;
        AttackPower = 20;
        Money = 200;
        Type = TuyulType.JaekYul;
    }

    private void Awake()
    {
        currentFormObject = this.gameObject; // Default ke bentuk asli
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

        // Special skill: dapat berubah menjadi Tuyul lain
        if (Random.value < 0.3f) // 30% chance
        {
            TransformToRandomTuyul();
            UseCurrentFormSpecialSkill(playerCharacter);
        }
        else
        {
            // basic attack
            NormalAttack(playerCharacter);
        }

        if (currentFormObject != null && currentFormObject != this.gameObject)
        {
            currentFormObject = this.gameObject; // Set ke bentuk asli
            Debug.Log($"{Name} kembali ke bentuk aslinya setelah menyerang!");
        }

        return false;
    }

    private void TransformToRandomTuyul()
    {
        System.Type[] tuyulTypes = { typeof(Aventurine), typeof(CheokYul), typeof(ChaengYul), typeof(MrRizzler) };
        System.Type randomTuyulType = tuyulTypes[Random.Range(0, tuyulTypes.Length)];

        // Hapus GameObject bentuk lama jika bukan JaekYul asli
        if (currentFormObject != null && currentFormObject != this.gameObject)
        {
            Destroy(currentFormObject); 
        }

        // Buat GameObject baru untuk bentuk baru
        currentFormObject = new GameObject(randomTuyulType.Name); // Buat GameObject baru
        var newTuyul = currentFormObject.AddComponent(randomTuyulType) as Tuyul;

        // Menambahkan komponen Animator
        var animator = currentFormObject.GetComponent<Animator>();
        if (animator == null)
        {
            animator = currentFormObject.AddComponent<Animator>(); // Tambahkan Animator baru jika belum ada
            Debug.LogWarning($"Animator tidak ditemukan pada {currentFormObject.name}, Animator baru ditambahkan.");
        }

        newTuyul.TuyulAnim = animator; // Hubungkan Animator ke Tuyul baru

        Debug.Log($"{Name} berubah menjadi {randomTuyulType.Name}!");
    }

    public override void NormalAttack(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

    private void UseCurrentFormSpecialSkill(Player playerCharacter)
    {
        Tuyul currentForm = currentFormObject.GetComponent<Tuyul>(); // Ambil komponen Tuyul dari bentuk aktif

        if (currentForm is Aventurine aventurine)
        {
            aventurine.UseTheGreatGatsby(playerCharacter);
        }
        else if (currentForm is MrRizzler rizzler)
        {
            rizzler.UseSeduceYouToDeath(playerCharacter);
        }
        else if (currentForm is CheokYul cheokYul)
        {
            cheokYul.UsePoison(playerCharacter);
        }
        else if (currentForm is ChaengYul chaengYul)
        {
            chaengYul.UseBeyondTheGrave(playerCharacter);
        }
        else
        {
            Debug.Log($"{Name} dalam bentuk {currentForm.Name} tidak memiliki special skill untuk digunakan!");
        }
    }
}
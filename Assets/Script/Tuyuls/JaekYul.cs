using UnityEngine;

public class JaekYul : Tuyul
{
    private Tuyul currentForm; // Form saat ini (bisa berubah jadi Tuyul lain)
    private System.Type originalForm; // Bentuk asli JaekYul

    public JaekYul()
    {
        Name = "Jaek Yul; Tuyul of All Trade, Master of All";
        maxHealth = 350;
        AttackPower = 20;
        Money = 200;

        // Default ke bentuk asli
        originalForm = typeof(JaekYul);
        currentForm = this;
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
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    private void TransformToRandomTuyul()
    {
        System.Type[] tuyulTypes = { typeof(Aventurine), typeof(CheokYul), typeof(ChaengYul), typeof(MrRizzler), typeof(Rolly), typeof(Polly) };  // rolly polly gimana?... ga ngerti dah gw konsep rolly polly ini
        System.Type randomTuyulType = tuyulTypes[Random.Range(0, tuyulTypes.Length)];

        // Transform menjadi Tuyul yang dipilih
        if (randomTuyulType != null)
        {
            if (randomTuyulType == typeof(Rolly) || randomTuyulType == typeof(Polly))
            {
                // Buat instance Rolly dan Polly sebagai partner
                Rolly rolly = new Rolly();
                Polly polly = new Polly();
                rolly.partner = polly;
                polly.partner = rolly;

                currentForm = (randomTuyulType == typeof(Rolly)) ? rolly : polly;
                
            }
            else
            {
                currentForm = (Tuyul)System.Activator.CreateInstance(randomTuyulType);
            }

            Debug.Log($"{Name} berubah menjadi {currentForm.Name}!");
        }
    }

    private void RevertToOriginalForm()
    {
        // Kembali ke bentuk asli
        currentForm = (Tuyul)System.Activator.CreateInstance(originalForm);
        Debug.Log($"{Name} kembali ke bentuk aslinya!");
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

    private void UseCurrentFormSpecialSkill(Player playerCharacter)
    {
        if (currentForm is Aventurine aventurine) {
            aventurine.UseTheGreatGatsby(playerCharacter);
        }
        else if (currentForm is MrRizzler rizzler) {
            rizzler.UseSeduceYouToDeath(playerCharacter);
        }
        else if (currentForm is CheokYul cheokYul) {
            cheokYul.UsePoison(playerCharacter);
        }
        else if (currentForm is ChaengYul chaengYul) {
            chaengYul.UseBeyondTheGrave(playerCharacter);
        }
        else if (currentForm is RollyPolly rollyPolly) {
            rollyPolly.UseTeamworkSkill(playerCharacter);
        }
        else {
            Debug.Log($"{Name} dalam bentuk {currentForm.Name} tidak memiliki special skill untuk digunakan!");
        }
    }
}
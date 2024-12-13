using UnityEngine;

public class RollyPolly : Tuyul    //jujur masih blm terlalu ngerti yang sepasang tuyul ini
{
    public RollyPolly partner; // Referensi ke pasangan

    public bool HasPartnerAlive => partner != null && partner.currentHealth > 0;

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

        // Special Skill: Teamwork is Dreamwork (20%)
        if (random.NextDouble() < 0.2)
        {
            UseTeamworkSkill(playerCharacter); 
        }
        else
        {
            // basic attack
            NormalRetaliation(playerCharacter);
        }

        return false;
    }

    private void NormalRetaliation(Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);
        TuyulAnim.SetTrigger("Throws");
        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");
    }

    public virtual void UseTeamworkSkill(Player playerCharacter)
    {
        if (HasPartnerAlive)   
        {
            int teamworkDamage = Mathf.RoundToInt(AttackPower) + Mathf.RoundToInt(partner.AttackPower);
            playerCharacter.TakeDamage(teamworkDamage);
            Debug.Log($"{Name} dan {partner.Name} menggunakan 'Teamwork is Dreamwork'! Pemain menerima {teamworkDamage} damage!");

            // animasi rolly polly nyerang berdua (?)
        }
        else
        {
            Debug.Log($"{Name} tidak dapat menggunakan 'Teamwork is Dreamwork' karena partner telah mati!");
        }
    }
    
    public virtual int GetAttackPower()
    {
        return AttackPower; // Default attack power
    }
}
using System.Collections;
using UnityEngine;

public class ChaengYul : Tuyul
{
    public Animator StoneThrow;
    public Renderer Stone;
    public Animator Heal;
    public Renderer healAttribute;

    void Start()
    {
        Stone.enabled = false;
        healAttribute.enabled = false;
    }

    public ChaengYul()
    {
        Name = "ChaengYul; Bestie of Pocong";
        maxHealth = 200;
        AttackPower = 15;
        Money = 100;
        Type = TuyulType.ChaengYul;
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

        return false;
    }

    public override void EnemyAction(Player playerCharacter)
    {
        StartCoroutine(ExecuteEnemyAction(playerCharacter));
    }

    private IEnumerator ExecuteEnemyAction(Player playerCharacter)
    {
        if (random.NextDouble() < 0.4)
        {
            int stolenAmount = random.Next(1, 101);
            if (playerCharacter.CurrencyManager.DeductMoney(stolenAmount))
            {
                TuyulAnim.SetTrigger("TPBP");
                Debug.Log($"{Name} menggunakan jurus rahasia: 'Tangan Panjang, Badan Pendek'. Kamu kehilangan uang sebesar {stolenAmount}!");
                yield return new WaitForSeconds(1f);
            }
        }

        // Passive Skill: Cursed Hop (30% chance)
        if (currentHealth <= 100 && random.NextDouble() < 0.3)
        {
            yield return StartCoroutine(UseCursedHop());
        }

        // Special Skill: Beyond the Grave (20% chance)
        if (random.NextDouble() < 0.2)
        {
            yield return StartCoroutine(UseBeyondTheGrave(playerCharacter));
        }
        else
        {
            NormalAttack(playerCharacter);
        }
    }

    private IEnumerator UseCursedHop()
    {
        int healAmount = Mathf.RoundToInt(maxHealth * 0.15f); // Heal 15% dari max HP
        currentHealth += healAmount;
        TuyulAnim.SetTrigger("CursedHop");
        healAttribute.enabled = true; // Munculkan efek heal

        Debug.Log($"{Name} menggunakan 'Cursed Hop' dan memulihkan {healAmount} HP! Sisa HP: {currentHealth}");

        yield return StartCoroutine(HideEffectAfterAnimation(Heal, healAttribute, "Heal"));
    }

    public IEnumerator UseBeyondTheGrave(Player playerCharacter)
    {
        TuyulAnim.SetTrigger("Behind");
        int Ultimate = AttackPower * 2;
        yield return new WaitForSeconds(2f);

        if (random.NextDouble() < 0.2)
        {
            playerCharacter.currentHealth = 0;
            Debug.Log("Scare You To Death");
        }
        else
        {
            playerCharacter.TakeDamage(Ultimate);
            Debug.Log($"{Name} menggunakan jurus spesial 'Beyond the Grave'! {playerCharacter.Name} menerima {Ultimate} damage! Sisa HP: {playerCharacter.currentHealth}");
        }
    }

    public override void NormalAttack (Player playerCharacter)
    {
        playerCharacter.TakeDamage(AttackPower);

        StartCoroutine(ExecuteNormalAttack(playerCharacter));
    }
    public IEnumerator ExecuteNormalAttack(Player playerCharacter)
    {
        Stone.enabled = true;
        TuyulAnim.SetTrigger("OnThrow");

        Debug.Log($"{Name} mengeluarkan jurus 'Ketimpuk Batu' dan memberikan {AttackPower} damage! Sisa HP: {playerCharacter.currentHealth}");

        yield return StartCoroutine(HideEffectAfterAnimation(StoneThrow, Stone, "Bounce"));
    }

    private IEnumerator HideEffectAfterAnimation(Animator animator, Renderer effect, string animationName)
    {
        effect.enabled = true;
        Debug.Log($"Efek {effect.name} diaktifkan untuk animasi {animationName}.");
        while (!IsAnimationFinished(animator, animationName))
        {
            yield return null;
        }

        // Sembunyikan efek setelah animasi selesai
        effect.enabled = false;
        Debug.Log($"Efek {effect.name} disembunyikan setelah animasi {animationName} selesai.");
    }

    private bool IsAnimationFinished(Animator animator, string animationName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 2f;
    }
}

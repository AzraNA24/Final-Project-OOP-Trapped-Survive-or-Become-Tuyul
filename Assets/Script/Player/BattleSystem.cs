using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER_TURN, TUYUL_TURN, WON, LOST }
public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public GameObject Player;
    public GameObject Tuyul;
    public GameObject Aventurine;
    public GameObject mrRizzler;
    public GameObject RollyPolly;
    public GameObject ChaengYul;
    public GameObject CheokYul;
    public GameObject JaekYul;
    public Transform playerStation;
    public Transform tuyulStation;
    public Button buttonAnimator;
    private Player playerCharacter;
    private Tuyul enemyCharacter;
    private System.Random random = new System.Random();
    public int maxPotionsPerBattle = 5;
    private int potionCounter;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        potionCounter = 0;

        GameObject selectedTuyulPrefab = null;

        if (PlayerAttack.currentTuyulName == "Aventurine")
        {
            selectedTuyulPrefab = Aventurine;
        }
        else if (PlayerAttack.currentTuyulName == "Mr.Rizzler")
        {
            selectedTuyulPrefab = mrRizzler;
        }
        else if (PlayerAttack.currentTuyulName == "RollyPolly")
        {
            selectedTuyulPrefab = RollyPolly;
        }
        else if (PlayerAttack.currentTuyulName == "ChaengYul")
        {
            selectedTuyulPrefab = ChaengYul;
        }
        else if (PlayerAttack.currentTuyulName == "CheokYul")
        {
            selectedTuyulPrefab = CheokYul;
        }
        else if (PlayerAttack.currentTuyulName == "JaekYul")
        {
            selectedTuyulPrefab = JaekYul;
        }

        GameObject playerGO = Instantiate(Player, playerStation);
        playerCharacter = playerGO.GetComponent<Player>();
        buttonAnimator.animator = playerGO.GetComponent<Animator>();

        GameObject enemyGO = Instantiate(selectedTuyulPrefab, tuyulStation);
        enemyCharacter = enemyGO.GetComponent<Tuyul>();
        if (enemyCharacter.TuyulAnim != null)
        {
            enemyCharacter.TuyulAnim.SetBool("TurnBased", true);
        }
        else
        {
            Debug.Log("Animator pada enemyCharacter tidak ditemukan!");
        }

        Debug.Log($"Pertarungan dimulai! {enemyCharacter.Name} muncul!");
        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYER_TURN;
        PlayerTurn();
    }


    //void SetupRollyPollyPair()
    //{
    //    Rolly rolly = Instantiate(Rolly, rollySpawnPoint).GetComponent<Rolly>();
    //    Polly polly = Instantiate(Polly, pollySpawnPoint).GetComponent<Polly>();

    //    rolly.partner = polly;
    //    polly.partner = rolly;
    //}

    void PlayerTurn()
    {
        // Terapkan efek poison hanya untuk CheokYul
        if (enemyCharacter is CheokYul cheokyul && enemyCharacter != null) // Cek apakah Tuyul adalah JaekYul
        {
            // cheokyul.ApplyPoison(playerCharacter); // Jalankan efek poison
        }
        if (enemyCharacter is MrRizzler mrRizzler && mrRizzler.DebuffRoundsLeft == 0 && playerCharacter.criticalChance < 0.3f)
        {
            mrRizzler.RemoveDebuff(playerCharacter);
        }

        Debug.Log("Pilih tindakan: Serang, Jarak Jauh, atau Potion");
    }

    public void OnShortRangeAttackButton()
    {
        if (state != BattleState.PLAYER_TURN)
            return;
        StartCoroutine(ShortRangeAttack());
        Debug.Log("Penyerangan selesai dilakukan");

    }

    IEnumerator ShortRangeAttack()
    {

        yield return new WaitUntil(() => !buttonAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("ShortRange"));
        int AttackPower = Mathf.CeilToInt(playerCharacter.CurrencyManager.TotalMoney * 0.09f);
        bool isCritical = random.NextDouble() < playerCharacter.criticalChance;
        int finalAttackPower = isCritical ? AttackPower * 2 : AttackPower;

        if (isCritical)
        {
            Debug.Log($"Serangan Critical! Kamu menyerang musuh dengan serangan jarak dekat sebesar {finalAttackPower}!");
        }
        else
        {
            Debug.Log($"Kamu menyerang musuh dengan serangan jarak dekat sebesar {finalAttackPower}!");
        }

        bool isDead = enemyCharacter.TakeDamage(finalAttackPower, playerCharacter);

        if (enemyCharacter is Aventurine aventurine)
        {
            aventurine.FUA(playerCharacter, isCritical);
        }

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.TUYUL_TURN;
            StartCoroutine(EnemyTurn());
        }
    }

    public void OnLongRangeAttackButton()
    {
        if (state != BattleState.PLAYER_TURN)
            return;
        StartCoroutine(LongRangeAttack());
    }

    IEnumerator LongRangeAttack()
    {
        int AttackPower = Mathf.CeilToInt(playerCharacter.CurrencyManager.TotalMoney * 0.15f);
        bool isCritical = random.NextDouble() < playerCharacter.criticalChance + 0.1f;
        int finalAttackPower = isCritical ? AttackPower * 2 : AttackPower;

        if (!playerCharacter.HasBullets())
        {
            Debug.Log("Peluru tidak cukup untuk serangan jarak jauh!");
            yield break;
        }

        if (random.NextDouble() < 0.3)
        {
            Debug.Log($"You Missed!");
            state = BattleState.TUYUL_TURN;
            StartCoroutine(EnemyTurn());
        }
        else
        {

            if (isCritical)
            {
                Debug.Log($"Serangan Critical! Kamu menyerang musuh dengan serangan jarak dekat sebesar {finalAttackPower}!");
            }
            else
            {
                Debug.Log($"Kamu menyerang musuh dengan serangan jarak dekat sebesar {finalAttackPower}!");
            }
            bool isDead = enemyCharacter.TakeDamage(finalAttackPower, playerCharacter);

            if (enemyCharacter is Aventurine aventurine)
            {
                aventurine.FUA(playerCharacter, isCritical);
            }

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.TUYUL_TURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        Debug.Log($"{enemyCharacter.Name} sedang menyerang!");

        yield return new WaitForSeconds(1f);

        enemyCharacter.EnemyAction(playerCharacter);

        yield return new WaitForSeconds(2f);

        if (playerCharacter.currentHealth <= 0)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYER_TURN;
            PlayerTurn();
        }
    }


    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            playerCharacter.CurrencyManager.AddMoney(enemyCharacter.Money);
            Debug.Log("Kamu menang!");
            PlayerPrefs.SetInt($"{enemyCharacter.Name}_Defeated", 1);
            SceneManagerController.Instance.ReturnToLastScene();
            FindObjectOfType<PlayerManager>()?.RespawnPlayer();

        }
        else if (state == BattleState.LOST)
        {
            Debug.Log("Kamu kalah!");
        }
    }

    public void OnPotionButton()
    {
        if (state != BattleState.PLAYER_TURN)
            return;

        if (potionCounter >= maxPotionsPerBattle)
        {
            Debug.Log("Kamu telah menggunakan semua potion yang tersedia untuk pertempuran ini, player kembung!");
            return;
        }

        if (playerCharacter.HasPotion())
        {
            StartCoroutine(UsePotion());
        }
        else
        {
            Debug.Log("Tidak ada potion yang tersedia!");
        }
    }

    IEnumerator UsePotion()
    {
        potionCounter++;
        playerCharacter.UsePotion();
        yield return new WaitForSeconds(1f);

        state = BattleState.TUYUL_TURN;
        StartCoroutine(EnemyTurn());
    }
}
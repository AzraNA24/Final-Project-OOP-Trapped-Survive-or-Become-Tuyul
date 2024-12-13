// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public enum BattleState{ START, PLAYER_TURN, TUYUL_TURN, WON, LOST}
// public class BattleSystem : MonoBehaviour
// {
//     public BattleState state;
//     public GameObject Player;
//     public GameObject Tuyul;
//     public Transform playerStation;
//     public Transform tuyulStation;
//     public Button buttonAnimator;
//     private Player playerCharacter;
//     private Aventurine enemyCharacter;
//     private System.Random random = new System.Random();
//     void Start()
//     {
//         state = BattleState.START;
//         StartCoroutine(SetupBattle());
//     }

//     IEnumerator SetupBattle()
//     {
//         GameObject playerGO = Instantiate(Player, playerStation);
//         playerCharacter = playerGO.GetComponent<Player>();
//         buttonAnimator.animator = playerGO.GetComponent<Animator>();

//         GameObject enemyGO = Instantiate(Tuyul, tuyulStation);
//         enemyCharacter = enemyGO.GetComponent<Aventurine>();
//         if (enemyCharacter == null)
// {
//     Debug.LogError("Prefab musuh tidak memiliki komponen Aventurine!");
// }

//         Debug.Log($"Pertarungan dimulai! {enemyCharacter.Name} muncul!");

//         yield return new WaitForSeconds(2f);

//         state = BattleState.PLAYER_TURN;
//         PlayerTurn();
//     }

//     void PlayerTurn()
//     {
//         Debug.Log("Pilih tindakan: Serang, Jarak Jauh, atau Potion");
//     }

//     public void OnShortRangeAttackButton()
//     {
//         if (state != BattleState.PLAYER_TURN)
//             return;
//         StartCoroutine(ShortRangeAttack());
//         Debug.Log("Penyerangan selesai dilakukan");
        
//     }

//     IEnumerator ShortRangeAttack()
//     {
        
//         yield return new WaitUntil(() => !buttonAnimator.animator.GetCurrentAnimatorStateInfo(0).IsName("ShortRange"));
//         int AttackPower = Mathf.CeilToInt(playerCharacter.CurrencyManager.TotalMoney * 0.09f);
//         Debug.Log($"Kamu menyerang musuh dengan serangan jarak dekat sebesar {AttackPower}!");
//         bool isDead = enemyCharacter.TakeDamage(AttackPower, playerCharacter);

//         yield return new WaitForSeconds(2f);

//         if (isDead)
//         {
//             state = BattleState.WON;
//             EndBattle();
//         }
//         else
//         {
//             state = BattleState.TUYUL_TURN;
//             StartCoroutine(EnemyTurn());
//         }
//     }

//     public void OnLongRangeAttackButton()
//     {
//         if (state != BattleState.PLAYER_TURN)
//             return;
//         StartCoroutine(LongRangeAttack());
//     }

//     IEnumerator LongRangeAttack()
//     {
//         int AttackPower = Mathf.CeilToInt(playerCharacter.CurrencyManager.TotalMoney * 0.15f);

//         if (!playerCharacter.HasBullets())
//         {
//             Debug.Log("Peluru tidak cukup untuk serangan jarak jauh!");
//             yield break;
//         }

//         if (random.NextDouble() < 0.3)
//         {
//                 Debug.Log($"You Missed!");
//                 state = BattleState.TUYUL_TURN;
//                 StartCoroutine(EnemyTurn());
//         } else
//         {
//             Debug.Log($"Kamu menyerang musuh dengan serangan jarak jauh sebesar {AttackPower}!");
//             bool isDead = enemyCharacter.TakeDamage(AttackPower, playerCharacter);

//             yield return new WaitForSeconds(2f);

//             if (isDead)
//             {
//                 state = BattleState.WON;
//                 EndBattle();
//             }
//             else
//             {
//                 state = BattleState.TUYUL_TURN;
//                 StartCoroutine(EnemyTurn());
//             }
//         }
//     }

//     IEnumerator EnemyTurn()
//     {
//         Debug.Log($"{enemyCharacter.Name} menyerang!");

//         yield return new WaitForSeconds(2f);

//         bool isDead = playerCharacter.TakeDamage(enemyCharacter.AttackPower);

//         if (isDead)
//         {
//             state = BattleState.LOST;
//             EndBattle();
//         }
//         else
//         {
//             state = BattleState.PLAYER_TURN;
//             PlayerTurn();
//         }
//     }

//     void EndBattle()
//     {
//         if (state == BattleState.WON)
//         {
//             playerCharacter.CurrencyManager.AddMoney(enemyCharacter.Money);
//             Debug.Log("Kamu menang!");
//         }
//         else if (state == BattleState.LOST)
//         {
//             Debug.Log("Kamu kalah!");
//         }
//     }

//     public void OnPotionButton()
//     {
//         if (state != BattleState.PLAYER_TURN)
//             return;
//         StartCoroutine(UsePotion());
//     }

//     IEnumerator UsePotion()
//     {
//         Debug.Log("Kamu menggunakan potion dan memulihkan kesehatan!");
//         playerCharacter.Heal(20);

//         yield return new WaitForSeconds(2f);

//         Debug.Log($"{enemyCharacter.Name} memanfaatkan momen lengahmu!");

//         yield return new WaitForSeconds(2f);

//         state = BattleState.TUYUL_TURN;
//         StartCoroutine(EnemyTurn());
//     }
// }

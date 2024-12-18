using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask Layer;
    public AudioSource hitSound;
    public static string currentTuyulName;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
            hitSound.Play();
            Debug.Log("Player memukul");
        }
    }

    void Attack(){
        float direction = transform.localScale.x;
        animator.SetFloat("Horizontal", direction);
        animator.SetTrigger("Attack");

        Collider2D[] hitThing= Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, Layer);

        foreach (Collider2D Thing in hitThing)
        {
            string objectName = Thing.gameObject.name;
            string objectLayer = LayerMask.LayerToName(Thing.gameObject.layer);
            
            Debug.Log($"Detected: {Thing.gameObject.name}");

            if (Thing.isTrigger)
            {
                Debug.Log($"Trigger detected: {Thing.gameObject.name}");
            }

            if (objectLayer == "Tuyul")
            {
                currentTuyulName = Thing.gameObject.name;
                Debug.Log($"Tuyul detected: {currentTuyulName}! Switching to TurnBased scene...");
                SceneManagerController.Instance.SwitchScene("TurnBased", SceneManagerController.GameMode.TurnBased);
                return;
            }
            
            LootBox lootBox = Thing.GetComponent<LootBox>();
            if (lootBox != null)
            {
                lootBox.GenerateLoot();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(AttackPoint.position, AttackRange);
    }
}

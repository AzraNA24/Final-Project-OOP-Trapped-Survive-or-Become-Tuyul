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

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
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

            if (objectName == "Aventurine")
            {
                Debug.Log("Aventurine detected! Switching to TurnBased scene...");
                SceneManager.LoadScene("TurnBased");
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

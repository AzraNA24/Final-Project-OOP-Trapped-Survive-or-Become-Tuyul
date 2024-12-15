using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private ItemUI inventoryUI;

    [SerializeField]
    private CodexUI codexUI; // Referensi ke UI Codex, tambahkan melalui Inspector

    public int inventorySize = 10;
    public AudioSource Open;

    private void Start()
    {
        inventoryUI.InitializedItemUI(inventorySize);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!inventoryUI.isActiveAndEnabled)
            {
                if (!Open.isActiveAndEnabled)
                {
                    Open.enabled = true;
                }
                Open.Play();
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!codexUI.isActiveAndEnabled)
            {
                Open.Play();
                codexUI.Show();
            }
            else
            {
                codexUI.Hide();
            }
        }
    }
}

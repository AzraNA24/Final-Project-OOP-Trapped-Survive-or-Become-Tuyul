using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private ItemUI inventoryUI;

    [SerializeField]
    private CodexUI codexUI;

    public int inventorySize = 10;
    public AudioSource Open;
    public static InventoryController Instance;

    private void Awake()
    {
        // Validasi Null
        if (inventoryUI == null)
        {
            Debug.Log("InventoryUI is not assigned in the Inspector!");
        }

        if (codexUI == null)
        {
            Debug.Log("CodexUI is not assigned in the Inspector!");
        }

        if (Open == null)
        {
            Debug.Log("AudioSource (Open) is not assigned in the Inspector!");
        }
    }

    private void Update()
    {
        // Membuka/Tutup Inventory
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI != null)
            {
                if (!inventoryUI.isActiveAndEnabled)
                {
                    PlayOpenSound();
                    inventoryUI.Show();
                    Debug.Log("Inventory terbuka");
                }
                else
                {
                    inventoryUI.Hide();
                    Debug.Log("Inventory tertutup");
                }
            }
        }

        // Membuka/Tutup Codex
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (codexUI != null)
            {
                if (!codexUI.isActiveAndEnabled)
                {
                    PlayOpenSound();
                    codexUI.Show();
                    Debug.Log("Codex terbuka");
                }
                else
                {
                    codexUI.Hide();
                    Debug.Log("Codex tertutup");
                }
            }
        }
    }

    private void PlayOpenSound()
    {
        if (Open != null && !Open.isPlaying)
        {
            Open.Play();
        }
    }
}

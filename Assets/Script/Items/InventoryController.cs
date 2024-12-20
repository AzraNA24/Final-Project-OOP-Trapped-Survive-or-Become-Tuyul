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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Validasi Null
        if (inventoryUI == null)
        {
            Debug.LogError("InventoryUI is not assigned in the Inspector!");
        }

        if (codexUI == null)
        {
            Debug.LogError("CodexUI is not assigned in the Inspector!");
        }

        if (Open == null)
        {
            Debug.LogError("AudioSource (Open) is not assigned in the Inspector!");
        }
    }

    private void Start()
    {
        if (inventoryUI != null)
        {
            inventoryUI.InitializeInventoryUI(inventorySize);
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

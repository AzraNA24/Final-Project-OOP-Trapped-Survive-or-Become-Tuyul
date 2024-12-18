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

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
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
                Debug.Log("Inventory terbuka");
            }
            else
            {
                inventoryUI.Hide();
                Debug.Log("Inventory tertutup");
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

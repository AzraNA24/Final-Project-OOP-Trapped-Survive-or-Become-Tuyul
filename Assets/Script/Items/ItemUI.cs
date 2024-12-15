using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField]
    private InventoryItem itemprefab;
    [SerializeField]
    private RectTransform contentPanel;

    List<InventoryItem> ListofUIItems = new List<InventoryItem>();

    public void InitializedItemUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            InventoryItem Item = Instantiate(itemprefab, Vector3.zero, Quaternion.identity);
            Item.transform.SetParent(contentPanel);
            ListofUIItems.Add(Item);
        }
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItemData
    {
        public string Name;
        public string Description;
        public int Quantity;
    }

    public InventoryItemData[] items;

    public TextMeshProUGUI itemTitleText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemQuantityText;
    public Image targetImage;
    public Sprite[] itemImage;

    public static ItemUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // Initialize the items array here to avoid NullReferenceException
        items = new InventoryItemData[]
        {
            new InventoryItemData
            {
                Name = "Money Bag",
                Description = $"Trusty money bag to bagged away your stolen money. Can be used as a weapon too. Inside, there's money: {CurrencyManager.Instance?.TotalMoney ?? 0}",
                Quantity = 1
            },
            new InventoryItemData
            {
                Name = "Riffle",
                Quantity = 1,
                Description = "When money can't get you out, a trusty gun can always be a reliable friend."
            },
            new InventoryItemData
            {
                Name = "Bullets",
                Quantity = Player.Instance?.bullets ?? 0,
                Description = "Just make sure the cops never know about this."
            },
            new InventoryItemData
            {
                Name = "Health Potion",
                Quantity = Player.Instance?.potions ?? 0,
                Description = "Highly recommended for neck pain."
            },
        };
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetImageByIndex(int index)
    {
        if (index >= 0 && index < itemImage.Length)
        {
            targetImage.sprite = itemImage[index];
        }
        else
        {
            Debug.LogError("Invalid image index!");
        }
    }

    public void DisplayItemInfo(int index)
    {
        SetImageByIndex(index);
        InventoryItemData item = items[index];
        itemTitleText.text = item.Name;
        itemDescriptionText.text = item.Description;
        itemQuantityText.text = item.Quantity.ToString();
    }
}

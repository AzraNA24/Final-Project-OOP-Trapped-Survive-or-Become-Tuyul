using UnityEngine;

public enum ItemType
{
    MoneyBag,
    HealthPotion,
    Gun,
    Bullet
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemIcon;
    public int quantity;
}

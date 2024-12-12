using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string Name { get; set; } = "Unknown";
    public abstract void Use(GameObject character);
}
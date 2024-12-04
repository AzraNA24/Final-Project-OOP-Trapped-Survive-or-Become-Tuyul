using UnityEngine;

public class LootBox : MonoBehaviour
{
    public void GenerateLoot()
    {
        int money = Random.Range(5, 11);
        int potion = Random.Range(0, 3);
        int bullet = Random.Range(0, 4);

        Debug.Log($"Receive: Money = {money}, Potion = {potion}, Bullet = {bullet}");

        Destroy(gameObject);
    }
}

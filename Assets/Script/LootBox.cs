using UnityEngine;

public class LootBox : MonoBehaviour
{
    public void GenerateLoot()
    {
        int money = Random.Range(1, 4);
        int potion = Random.Range(0, 2);
        int bullet = Random.Range(0, 3);

        Debug.Log($"Receive: Money = {money}, Potion = {potion}, Bullet = {bullet}");

        Destroy(gameObject);
    }
}

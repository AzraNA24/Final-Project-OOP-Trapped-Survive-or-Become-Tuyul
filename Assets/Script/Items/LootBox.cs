using UnityEngine;

public class LootBox : MonoBehaviour
{
    public Player currency;
    public void GenerateLoot()
    {
        int money = Random.Range(1, 4);
        int potion = Random.Range(0, 2);
        int bullet = Random.Range(0, 3);

        Debug.Log($"Receive: Money = {money}, Potion = {potion}, Bullet = {bullet}");
        if (currency != null)
        {
            currency.CurrencyManager.AddMoney(money);
            currency.AddPotion(potion);
            currency.AddBullets(bullet);
            Debug.Log($"Uang sekarang {currency.CurrencyManager.TotalMoney}");
        }

        Destroy(gameObject);
    }
}

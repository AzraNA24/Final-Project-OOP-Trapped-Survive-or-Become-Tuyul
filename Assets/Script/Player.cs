using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    [Header("Character Stats")]
    public string Name = "Pemburu Harta Karun";
    public float Health = 100;
    //Buat CurrencyManager dulu
    // [Header("Currency Manager")]
    // public CurrencyManager CurrencyManager = new CurrencyManager();

    // Singleton instance
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("Character");
                    instance = obj.AddComponent<Player>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // CurrencyManager.TotalMoney = 100;
    }
}

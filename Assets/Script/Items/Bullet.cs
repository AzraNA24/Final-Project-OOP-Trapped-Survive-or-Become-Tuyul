using UnityEngine; // eh ini mau diisi apa deh

public class Bullet : Item
{
    public int BulletCount { get; private set; }

    public void SetBulletCount(int count)
    {
        BulletCount = count;
    }

    public override void Use(GameObject character)
    {
        Debug.LogWarning("Bullet tidak dapat digunakan langsung.");
    }
}
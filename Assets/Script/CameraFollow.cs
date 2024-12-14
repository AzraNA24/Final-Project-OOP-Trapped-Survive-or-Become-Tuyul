using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referensi ke player
    public Vector3 offset;   // Jarak antara kamera dan player

    void Start()
    {
        // Atur jarak offset jika belum diatur
        if (offset == Vector3.zero)
        {
            offset = transform.position - player.position;
        }
    }

    void LateUpdate()
    {
        // Update posisi kamera mengikuti player dengan offset
        transform.position = player.position + offset;
    }
}

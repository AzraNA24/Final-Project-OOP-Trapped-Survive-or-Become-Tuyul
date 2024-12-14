using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referensi ke Player
    public float smoothSpeed = 0.125f; // Kecepatan kamera mengikuti
    public Vector3 offset; // Jarak antara kamera dan player
    private Vector3 screenBounds; // Batas layar

    void Start()
    {
        // Ambil batas layar dalam world space
        Camera mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (player == null)
        {
            Debug.LogError("Player Transform is not assigned!");
        }
    }

    void LateUpdate()
    {
        if (player == null) return; // Pastikan player ada
        
        // Target posisi kamera berdasarkan posisi player + offset
        Vector3 targetPosition = player.position + offset;

        // Smooth kamera mengikuti posisi target
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        // Terapkan posisi kamera
        transform.position = smoothedPosition;

        // Opsional: Batasi gerakan kamera sesuai batas layar
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x, screenBounds.x);
        float clampedY = Mathf.Clamp(transform.position.y, -screenBounds.y, screenBounds.y);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}

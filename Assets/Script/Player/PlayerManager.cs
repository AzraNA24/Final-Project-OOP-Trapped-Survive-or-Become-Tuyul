using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerMode { Exploration, TurnBased }
    public PlayerMode currentMode;
    

    // Referensi prefab
    public GameObject explorationPlayerPrefab;
    public GameObject turnBasedPlayerPrefab;
    public Vector3 respawnPosition;
    public CinemachineVirtualCamera Camera;
    public InventoryController inventory;

    private GameObject activePlayer;

    void Start()
    {
        SwitchMode(PlayerMode.Exploration);
    }

    public void SwitchMode(PlayerMode mode)
    {
        if (activePlayer != null)
        {
            Destroy(activePlayer);
        }
        if (mode == PlayerMode.Exploration)
        {
            if (explorationPlayerPrefab != null && activePlayer == null)
            {
                activePlayer = GameObject.FindWithTag("Player");
            }
            if (activePlayer == null)
            {
                activePlayer = Instantiate(explorationPlayerPrefab, transform.position, Quaternion.identity);
            }
        }
        else if (mode == PlayerMode.TurnBased)
        {
            activePlayer = Instantiate(turnBasedPlayerPrefab, transform.position, Quaternion.identity);
        }

        currentMode = mode;

        if (Camera != null)
        {
            Camera.Follow = activePlayer.transform;
        }
    }
    public void SetRespawnPosition(Vector3 position)
    {
        respawnPosition = position;
    }

    public void RespawnPlayer()
    {
        if (activePlayer != null)
        {
            activePlayer.transform.position = respawnPosition;
        }
    }
}

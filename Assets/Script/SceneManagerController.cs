using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController Instance;

    public enum GameMode { Exploration, TurnBased }
    public string lastSceneName;
    private GameMode currentMode;

    public GameObject playerController;
    private GameObject playerControllerInstance;

    private Vector2 playerPosition; // Menyimpan posisi Player terakhir

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Instantiate Player Controller di awal permainan
        playerControllerInstance = Instantiate(playerController);
        currentMode = GameMode.Exploration;
    }

    public void SwitchScene(string sceneName, GameMode mode)
    {
        if (mode == GameMode.TurnBased)
        {
            // Simpan posisi eksplorasi sebelum masuk Turn-Based
            FindObjectOfType<PlayerManager>()?.SaveExplorationStartPosition();
        }

        currentMode = mode;
        if (mode == GameMode.TurnBased)
        {
            lastSceneName = SceneManager.GetActiveScene().name;
        }
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Reposition player setelah scene baru dimuat
        RepositionPlayer();
        PlayerManager prefabController = playerControllerInstance.GetComponent<PlayerManager>();
        if (prefabController != null)
        {
            if (currentMode == GameMode.Exploration)
                prefabController.SwitchMode(PlayerManager.PlayerMode.Exploration);
            else
                prefabController.SwitchMode(PlayerManager.PlayerMode.TurnBased);
        }
    }

    private void SavePlayerPosition()
    {
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        GameObject activePlayer = GameObject.FindWithTag("Player");

        if (playerManager != null && activePlayer != null)
        {
            playerPosition = activePlayer.transform.position; 
            playerManager.SavePlayerPosition(playerPosition); 
            Debug.Log($"Posisi pemain disimpan: {playerPosition}");
        }
    }

    private void RepositionPlayer()
    {
        // Reposisi Player di scene baru
        GameObject activePlayer = GameObject.FindWithTag("Player");
        if (activePlayer != null)
        {
            activePlayer.transform.position = playerPosition;
            Debug.Log($"Posisi pemain dipulihkan: {playerPosition}");
        }
    }
    
    public void ReturnToLastScene()
    {
        if (!string.IsNullOrEmpty(lastSceneName))
        {
            SceneManager.LoadScene(lastSceneName);
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                // Pulihkan posisi Player
                FindObjectOfType<PlayerManager>()?.RestoreExplorationStartPosition();

                // Hapus Tuyul yang sudah dikalahkan
                GameObject[] tuyuls = GameObject.FindGameObjectsWithTag("Tuyul");
                foreach (GameObject tuyul in tuyuls)
                {
                    string tuyulName = tuyul.name;
                    FindObjectOfType<PlayerManager>()?.CheckAndRemoveDefeatedTuyuls(tuyul, tuyulName);
                }
            };
        }
        else
        {
            Debug.LogWarning("Last scene name is empty!");
        }
    }
}

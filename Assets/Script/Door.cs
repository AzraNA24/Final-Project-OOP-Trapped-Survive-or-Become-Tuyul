using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    [SerializeField] private string finalScene; // Scene terakhir
    [SerializeField] private List<string> middleScenes; // Daftar scene random
    private static HashSet<string> visitedScenes = new HashSet<string>(); 
    private static bool isFinalScene = false; 

        private void Awake()
    {
        //Biar nggak keulang ruangannya
        LoadVisitedScenes();
    }
    private void OnApplicationQuit()
    {
        // Simpan data kunjungan
        SaveVisitedScenes();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isFinalScene)
            {
                SceneManagerController.Instance.SwitchScene(finalScene, SceneManagerController.GameMode.Exploration);
            }
            else
            {
                string nextScene = GetRandomScene();
                if (nextScene == "")
                {
                    isFinalScene = true;
                    SceneManagerController.Instance.SwitchScene(finalScene, SceneManagerController.GameMode.Exploration);
                }
                else
                {
                    visitedScenes.Add(nextScene);
                    SceneManagerController.Instance.SwitchScene(nextScene, SceneManagerController.GameMode.Exploration);
                }
            }
        }
    }

    private string GetRandomScene()
    {
        List<string> unvisitedScenes = new List<string>();
        foreach (string scene in middleScenes)
        {
            if (!visitedScenes.Contains(scene) && !IsRoomCleared(scene))
            {
                unvisitedScenes.Add(scene);
            }
        }

        if (unvisitedScenes.Count > 0)
        {
            int randomIndex = Random.Range(0, unvisitedScenes.Count);
            return unvisitedScenes[randomIndex];
        }

        return ""; // Semua ruangan telah dikunjungi atau selesai
    }

    private bool IsRoomCleared(string sceneName)
    {
        return PlayerPrefs.GetInt($"{sceneName}_Cleared", 0) == 1;
    }
    private void SaveVisitedScenes()
    {
        PlayerPrefs.SetString("VisitedScenes", string.Join(",", visitedScenes));
        PlayerPrefs.Save();
    }

    private void LoadVisitedScenes()
    {
        string savedData = PlayerPrefs.GetString("VisitedScenes", "");
        if (!string.IsNullOrEmpty(savedData))
        {
            visitedScenes = new HashSet<string>(savedData.Split(','));
        }
    }
}
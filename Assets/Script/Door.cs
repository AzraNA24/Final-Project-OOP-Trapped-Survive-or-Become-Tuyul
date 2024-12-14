using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Door : MonoBehaviour
{
    [SerializeField] private string finalScene; // Scene terakhir
    [SerializeField] private List<string> middleScenes; // Daftar scene random
    private static HashSet<string> visitedScenes = new HashSet<string>(); 
    private static bool isFinalScene = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isFinalScene)
            {
                SceneManager.LoadScene(finalScene);
            }
            else
            {
                string nextScene = GetRandomScene();
                if (nextScene == "")
                {
                    isFinalScene = true;
                    SceneManager.LoadScene(finalScene);
                }
                else
                {
                    visitedScenes.Add(nextScene);
                    SceneManager.LoadScene(nextScene);
                }
            }
        }
    }

    private string GetRandomScene()
    {
        List<string> unvisitedScenes = new List<string>();
        foreach (string scene in middleScenes)
        {
            if (!visitedScenes.Contains(scene))
            {
                unvisitedScenes.Add(scene);
            }
        }

        if (unvisitedScenes.Count > 0)
        {
            int randomIndex = Random.Range(0, unvisitedScenes.Count);
            return unvisitedScenes[randomIndex];
        }

        return "";
    }
}
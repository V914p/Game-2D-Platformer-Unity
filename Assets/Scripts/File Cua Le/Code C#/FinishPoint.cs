using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class FinishPoint : MonoBehaviour
{
    private AudioManager audioManager;
    private void Awake()
    {
        audioManager = GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager = FindObjectOfType<AudioManager>();
        if (collision.CompareTag("Player"))
        {
            UnlockNewLevel();
            GoToNextLevel();
            audioManager.PlayNextLevelSound();
        }    
    }
    void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

    public void GoToNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            string nextSceneName = Path.GetFileNameWithoutExtension(nextScenePath);

            PlayerPrefs.SetString("NextScene", nextSceneName);
            SceneManager.LoadScene("LoadingScene");
        }
    }
}

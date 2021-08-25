using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AudioManager audioManager;
    public static GameManager instance;
    private string sceneName;

    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        SceneName();
        OnSceneLoad(sceneName);
    }

    private void OnLevelWasLoaded()
    {
        SceneName();
        OnSceneLoad(sceneName);
    }

    private void Update()
    {
        OnSceneUpdate(sceneName);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public string SceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        return sceneName;
    }

    public void OnSceneLoad(string sceneName)
    {
        if (sceneName == "Main Menu")
        {
            audioManager.Play("Main Theme v2");
            audioManager.StopPlay("Main Theme v1");
        }
        else if (sceneName == "Pause Menu")
        {
        }
        else if (sceneName == "Credits")
        {
            audioManager.StopPlay("Main Theme v2");
            audioManager.Play("Main Theme v1");
        }
        else if (sceneName == "Test Level")
        {
        }
    }

    public void OnSceneUpdate(string sceneName)
    {
        if (sceneName == "Main Menu")
        {
        }
        else if (sceneName == "Pause Menu")
        {
        }
        else if (sceneName == "Credits")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadScene("Main Menu");
            }
        }
        else if (sceneName == "Test Level")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadScene("Pause Menu");
            }
        }
    }
}

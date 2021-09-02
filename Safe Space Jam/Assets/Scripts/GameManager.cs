using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

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
            audioManager.Play("Menu Music");
            audioManager.StopPlay("Credits");
            audioManager.StopPlay("Level Music");            
        }
        else if (sceneName == "Credits")
        {
            audioManager.Play("Credits");
            audioManager.StopPlay("Menu Music");            
            audioManager.StopPlay("Level Music");
        }
        else if (sceneName == "Level 1")
        {
            audioManager.Play("Level Music");
            audioManager.StopPlay("Menu Music");
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
        else if (sceneName == "Level 1")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                LoadScene("Main Menu");
            }
        }
    }

    public void Quitegame()
    {
        Application.Quit();
    }
}
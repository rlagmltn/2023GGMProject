using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoSingleton<SceneMgr>
{
    private eSceneName sceneState = eSceneName.None;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        ChangeSceneState();
    }

    public void ChangeSceneState()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        sceneState = eSceneName.None;
        for (int i = 0; i < (int)eSceneName.MaxCount; i++)
        {
            if (sceneName == ((eSceneName)i).ToString())
            {
                sceneState = (eSceneName)i;
            }
        }
    }

    public void LoadScene(eSceneName state)
    {
        SceneManager.LoadScene(state + "Scene");
        ChangeSceneState();
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public AsyncOperation LoadSceneAsync(string name)
    {
        return SceneManager.LoadSceneAsync(name);
    }
}
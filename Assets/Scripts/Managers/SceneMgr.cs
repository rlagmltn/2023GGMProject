using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoSingleton<SceneMgr>
{
    public void LoadScene(eSceneName state)
    {
        SceneManager.LoadScene(state + "Scene");
    }
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
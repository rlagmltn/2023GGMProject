using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameManager : MonoBehaviour
{
    public SCENE scene;
    public Scene[] scenes;
    protected void Awake()
    {
        ContainScene();
        CheckScene();
    }

    protected void Start()
    {

    }

    public void Init()
    {

    }
    protected void Update()
    {

    }
    public void ContainScene()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
            Debug.Log(scenes[i].name);
        }
    }
    public void CheckScene()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MGUI : MonoSingleton<MGUI>
{
    public Image escPanel;
    public GameObject win;
    public GameObject lose;
    private void Start()
    {
        escPanel = GetComponentInChildren<Image>();
        escPanel.gameObject.SetActive(false);

        win = GameObject.Find("Win");
        lose = GameObject.Find("Lose");
        win?.SetActive(false);
        lose?.SetActive(false);
    }

    private void Update()
    {
        ToUpdate();
    }

    public void ToUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleEscPanel();
        }
    }

    public void ToggleEscPanel()
    {
        escPanel.gameObject.SetActive(!escPanel.enabled);
    }

    public void MoveToTitleScene()
    {
        SceneManager.LoadScene(eSceneName.Title.ToString() + "Scene");
    }

    public void MoveToMapScene()
    {
        SceneManager.LoadScene(eSceneName.Map.ToString() + "Scene");
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene(eSceneName.Main.ToString() + "Scene");
    }

    public void MoveToLoadingScene()
    {
        SceneManager.LoadScene(eSceneName.Loading.ToString() + "Scene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        win?.SetActive(true);
    }

    public void GameClear()
    {
        Time.timeScale = 0f;
        lose?.SetActive(true);
    }
}

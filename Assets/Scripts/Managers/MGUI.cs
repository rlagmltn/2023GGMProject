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
        SceneMgr.Instance.LoadScene(eSceneName.Title);
    }

    public void MoveToMapScene()
    {
        SceneMgr.Instance.LoadScene(eSceneName.Map);
    }
    public void MoveToSelectScene()
    {
        SceneMgr.Instance.LoadScene(eSceneName.Select);
    }

    public void MoveToMainScene()
    {
        SceneMgr.Instance.LoadScene(eSceneName.Main);
    }

    public void MoveToLoadingScene()
    {
        SceneMgr.Instance.LoadScene(eSceneName.Loading);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        lose?.SetActive(true);
    }

    public void GameClear()
    {
        win?.SetActive(true);
    }
}

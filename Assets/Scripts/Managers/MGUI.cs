using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MGUI : MonoSingleton<MGUI>
{
    private Image escPanel;
    private void Start()
    {
        escPanel = GetComponentInChildren<Image>();
        escPanel.gameObject.SetActive(false);
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
        escPanel.gameObject.SetActive(!escPanel.gameObject.activeSelf);
    }

    public void MoveToTitleScene()
    {
        SceneManager.LoadScene(0);
    }

    public void MoveToMapScene()
    {
        SceneManager.LoadScene(1);
    }

    public void MoveToMainScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

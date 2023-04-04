using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameUI : MonoBehaviour
{
    [SerializeField] Button gameStartButton;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// Initialize Function
    /// </summary>
    void Init()
    {
        gameStartButton.onClick.AddListener(ChangeScene);
    }

    void ChangeScene()
    {
        SceneMgr.Instance.LoadScene(eSceneName.Select);
        Debug.Log("Change Scene");
    }
}

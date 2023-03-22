using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] Button gameStartButton;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// ó������ �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    void Init()
    {
        AddButtonEvent(gameStartButton, ChangeScene);
    }

    void ChangeScene()
    {
        //MGScene.Instance.ChangeScene(eSceneName.Select);
        SceneManager.LoadScene("SelectScene");
        Debug.Log("Change Scene to SelectScene");
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}

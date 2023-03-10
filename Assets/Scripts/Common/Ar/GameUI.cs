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
    /// ó������ �ʱ�ȭ�ϴ� �Լ�
    /// </summary>
    void Init()
    {
        AddButtonEvent(gameStartButton, ChangeScene);
    }

    void ChangeScene()
    {
        MGScene.Instance.ChangeScene(eSceneName.Select);
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}

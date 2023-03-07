using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene���� ���� �Ű��ִ� �Լ�

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        AddButtonEvent();
    }

    void AddButtonEvent()
    {
        startButton.onClick.AddListener(() => MGScene.Instance.ChangeScene(eSceneName.Loading));
    }
}

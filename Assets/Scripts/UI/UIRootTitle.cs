using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene���� ���� �Ű��ִ� �Լ�

    //private void Awake()
    //{
    //    InitFirst();
    //}

    private void Start()
    {
        MGScene.Instance.ChangeScene(eSceneName.Game);
    }

    void InitFirst()
    {
        startButton.onClick.AddListener(() => MGScene.Instance.ChangeScene(eSceneName.Game));
    }
}

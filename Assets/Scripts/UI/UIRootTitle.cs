using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene으로 씬을 옮겨주는 함수

    private void Awake()
    {
        InitFirst();
    }

    void InitFirst()
    {
        startButton.onClick.AddListener(() => MGScene.Instance.ChangeScene(eSceneName.Game));
    }
}

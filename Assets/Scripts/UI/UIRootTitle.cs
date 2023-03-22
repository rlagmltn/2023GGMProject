using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene으로 씬을 옮겨주는 함수

    //private void Awake()
    //{
    //    InitFirst();
    //}

    private void Start()
    {
        //MGScene.Instance.ChangeScene(eSceneName.Game);
        SceneManager.LoadScene("MainScene");
        Debug.Log("Change Scene to MainScene");
    }

    void InitFirst()
    {
        //startButton.onClick.AddListener(() => MGScene.Instance.ChangeScene(eSceneName.Game));
    }
}

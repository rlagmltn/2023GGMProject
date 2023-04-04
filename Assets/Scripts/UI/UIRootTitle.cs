using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene으로 씬을 옮겨주는 함수
    private void Start()
    {
        Init();
    }

    void Init()
    {
        SceneMgr.Instance.LoadScene("MainScene");
        Debug.Log("Change Scene to MainScene");
    }
}

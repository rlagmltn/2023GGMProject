using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRootTitle : MonoBehaviour 
{
    public Button startButton;    //GameScene���� ���� �Ű��ִ� �Լ�
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

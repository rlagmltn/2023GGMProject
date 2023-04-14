using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManger : MonoBehaviour
{
    [SerializeField] private Button ClearButton;
    [SerializeField] private Transform BackGroudnPannel;
    [SerializeField] private Transform ClearPannel;

    private void Start()
    {
        ButtonInit();
    }

    internal void EventClear() //�̺�Ʈ�� Ŭ��������� ����� â
    {
        BackGroudnPannel.gameObject.SetActive(true);
        ClearPannel.gameObject.SetActive(true);
    }

    void ButtonInit()
    {
        ClearButton.onClick.RemoveAllListeners();
        ClearButton.onClick.AddListener(StageClear);
    }

    void StageClear()
    {
        Global.EnterStage.IsCleared = true;
        SceneMgr.Instance.LoadScene(eSceneName.Map);
    }
}

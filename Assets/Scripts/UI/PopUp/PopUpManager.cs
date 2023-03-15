using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class ButtonAndPopUp
{
    public Button button;
    public Transform popUp;
    public Button quitButton;
}

public class PopUpManager : MonoBehaviour
{
    [SerializeField] private List<ButtonAndPopUp> BAP;
    [SerializeField] private Transform backgroundPanel;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 처음 초기화 해주는 함수
    /// </summary>
    private void Init()
    {
        ButtonInit();
        BackGroundPanelActiveFalse();
    }
    
    /// <summary>
    /// 버튼 초기화
    /// </summary>
    private void ButtonInit()
    {
        foreach(ButtonAndPopUp btn in BAP)
        {
            SetButtonPopUp(btn.button, btn.popUp);

            RemoveButtonEvnets(btn.button);
            RemoveButtonEvnets(btn.quitButton);

            AddButtonEvent(btn.button, btn.button.GetComponent<PopUpHolder>().PopUpUI);
            AddButtonEvent(btn.quitButton, btn.button.GetComponent<PopUpHolder>().PopDownUI);

            AddButtonEvent(btn.button, BackGroundPanelActiveTrue);
            AddButtonEvent(btn.quitButton, BackGroundPanelActiveFalse);
        }
    }

    /// <summary>
    /// 백그라운드 패널을 켜주는 역할
    /// </summary>
    void BackGroundPanelActiveTrue()
    {
        backgroundPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// 백그라운드 패널을 꺼주는 역할
    /// </summary>
    void BackGroundPanelActiveFalse()
    {
        backgroundPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 버튼에 팝업창을 홀드 시키는 함수
    /// </summary>
    /// <param name="button"></param>
    /// <param name="transform"></param>
    void SetButtonPopUp(Button button, Transform transform)
    {
        button.GetComponent<PopUpHolder>().SetPopUpObj(transform);
    }

    /// <summary>
    /// 버튼의 이벤트를 전부 지우는 함수
    /// </summary>
    /// <param name="button"></param>
    private void RemoveButtonEvnets(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 버튼에 이벤트를 붙여주는 함수
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    private void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

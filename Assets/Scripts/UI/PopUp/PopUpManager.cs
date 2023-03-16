using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class ButtonAndPopUp
{
    //누를 버튼
    public Button button;
    //떠오를 팝압창
    public Transform popUp;
    //닫는 버튼
    public Button quitButton;
}

/// <summary>
/// 모든 팝업창을 관리하는 최상위 함수
/// </summary>
public class PopUpManager : MonoBehaviour
{
    [SerializeField] private List<ButtonAndPopUp> BAP;
    [SerializeField] private Transform backgroundPanel;
    [SerializeField] private Transform backgroundPanel2;

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
    /// UI를 리로드 하는게 필요한 패널 리로드 시키는 함수
    /// </summary>
    void ReloadUIs()
    {
        HelpPannel.Instance.UpdateUI();
    }

    /// <summary>
    /// UI를 초기화 하는게 필요한 패널을 초기화 시키는 함수
    /// </summary>
    void ResetUIs()
    {
        HelpPannel.Instance.ResetPannelNum();
    }

    /// <summary>
    /// 백그라운드 패널을 켜주는 역할
    /// </summary>
    void BackGroundPanelActiveTrue()
    {
        if (backgroundPanel.gameObject.activeSelf)
        {
            BackGroundPanelActiveTrue2();
            return;
        }

        backgroundPanel.gameObject.SetActive(true);
        ReloadUIs();
    }

    void BackGroundPanelActiveTrue2()
    {
        backgroundPanel2.gameObject.SetActive(true);
        ReloadUIs();
    }

    /// <summary>
    /// 백그라운드 패널을 꺼주는 역할
    /// </summary>
    void BackGroundPanelActiveFalse()
    {
        if (backgroundPanel2.gameObject.activeSelf)
        {
            BackGroundPanelActiveFalse2();
            return;
        }

        backgroundPanel.gameObject.SetActive(false);
        ResetUIs();
    }
    
    void BackGroundPanelActiveFalse2()
    {
        backgroundPanel2.gameObject.SetActive(false);
        ResetUIs();
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

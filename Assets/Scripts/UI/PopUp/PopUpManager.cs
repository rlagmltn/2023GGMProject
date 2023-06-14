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
    //public Button quitButton;
    public List<Button> Quitbuttons;
}

/// <summary>
/// 모든 팝업창을 관리하는 최상위 함수
/// </summary>
public class PopUpManager : MonoBehaviour
{
    [SerializeField] private List<ButtonAndPopUp> BAP;
    [SerializeField] private Transform backgroundPanel;
    [SerializeField] private Transform backgroundPanel2;
    [SerializeField] private List<ButtonAndPopUp> BAP2;

    [SerializeField] private ButtonAndPopUp EnterStageBAP;
    [SerializeField] private Transform AnotherPannel;
    [SerializeField] private Button AnotherCancel;
    [SerializeField] private Button AnotherClear;

    private void Awake()
    {
        Init();
    }

    /// <summary>
    /// 처음 초기화 해주는 함수
    /// </summary>
    private void Init()
    {
        ButtonInit(BAP);
        ButtonInit(BAP2);
        FirstBackGroundPanelActiveFalse();
    }
    
    /// <summary>
    /// 버튼 초기화
    /// </summary>
    private void ButtonInit(List<ButtonAndPopUp> buttonAndPopUps)
    {
        EnterStageBAP.button.onClick.RemoveAllListeners();
        EnterStageBAP.button.onClick.AddListener(EnterButtonClick);
        AnotherCancel.onClick.RemoveAllListeners();
        AnotherCancel.onClick.AddListener(EnterPannelPopDown);
        AnotherClear.onClick.RemoveAllListeners();
        AnotherClear.onClick.AddListener(ClearBtn);
        AnotherClear.onClick.AddListener(EnterPannelPopDown);

        for (int num = 0; num < EnterStageBAP.Quitbuttons.Count; num++)
        {
            RemoveButtonEvnets(EnterStageBAP.Quitbuttons[num]);
            AddButtonEvent(EnterStageBAP.Quitbuttons[num], EnterPannelPopDown);
            AddButtonEvent(EnterStageBAP.Quitbuttons[num], BackGroundPanelActiveFalse);
        }

        foreach (ButtonAndPopUp btn in buttonAndPopUps)
        {
            SetButtonPopUp(btn.button, btn.popUp);

            RemoveButtonEvnets(btn.button);
            
            for(int num = 0; num < btn.Quitbuttons.Count; num++)
            {
                RemoveButtonEvnets(btn.Quitbuttons[num]);
                AddButtonEvent(btn.Quitbuttons[num], btn.button.GetComponent<PopUpHolder>().PopDownUI);
                AddButtonEvent(btn.Quitbuttons[num], BackGroundPanelActiveFalse);
            }

            //RemoveButtonEvnets(btn.quitButton);

            AddButtonEvent(btn.button, btn.button.GetComponent<PopUpHolder>().PopUpUI);
            //AddButtonEvent(btn.quitButton, btn.button.GetComponent<PopUpHolder>().PopDownUI);

            AddButtonEvent(btn.button, BackGroundPanelActiveTrue);
            //AddButtonEvent(btn.quitButton, BackGroundPanelActiveFalse);
        }
    }

    void EnterButtonClick()
    {
        if (!SaveManager.Instance.GameData.IsPlayingGame) EnterStageBAP.popUp.gameObject.SetActive(true);
        else AnotherPannel.gameObject.SetActive(true);
        backgroundPanel.gameObject.SetActive(true);
    }

    void EnterPannelPopDown()
    {
        EnterStageBAP.popUp.gameObject.SetActive(false);
        AnotherPannel.gameObject.SetActive(false);
        backgroundPanel.gameObject.SetActive(false);
    }

    void ClearBtn()
    {
        SaveManager.Instance.GameData = new GameData();
        ArInventoryManager.Instance.Init();
        MainTestModeManager.Instance.ItemInvenReset();
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

    void FirstBackGroundPanelActiveFalse()
    {
        if (backgroundPanel2.gameObject.activeSelf)
        {
            BackGroundPanelActiveFalse2();
            return;
        }

        backgroundPanel.gameObject.SetActive(false);
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

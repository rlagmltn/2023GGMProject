using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public class ButtonAndPopUp
{
    //���� ��ư
    public Button button;
    //������ �˾�â
    public Transform popUp;
    //�ݴ� ��ư
    //public Button quitButton;
    public List<Button> Quitbuttons;
}

/// <summary>
/// ��� �˾�â�� �����ϴ� �ֻ��� �Լ�
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
    /// ó�� �ʱ�ȭ ���ִ� �Լ�
    /// </summary>
    private void Init()
    {
        ButtonInit(BAP);
        ButtonInit(BAP2);
        FirstBackGroundPanelActiveFalse();
    }
    
    /// <summary>
    /// ��ư �ʱ�ȭ
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
    /// UI�� ���ε� �ϴ°� �ʿ��� �г� ���ε� ��Ű�� �Լ�
    /// </summary>
    void ReloadUIs()
    {
        HelpPannel.Instance.UpdateUI();
    }

    /// <summary>
    /// UI�� �ʱ�ȭ �ϴ°� �ʿ��� �г��� �ʱ�ȭ ��Ű�� �Լ�
    /// </summary>
    void ResetUIs()
    {
        HelpPannel.Instance.ResetPannelNum();
    }

    /// <summary>
    /// ��׶��� �г��� ���ִ� ����
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
    /// ��׶��� �г��� ���ִ� ����
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
    /// ��ư�� �˾�â�� Ȧ�� ��Ű�� �Լ�
    /// </summary>
    /// <param name="button"></param>
    /// <param name="transform"></param>
    void SetButtonPopUp(Button button, Transform transform)
    {
        button.GetComponent<PopUpHolder>().SetPopUpObj(transform);
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ�� ���� ����� �Լ�
    /// </summary>
    /// <param name="button"></param>
    private void RemoveButtonEvnets(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ�� �ٿ��ִ� �Լ�
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    private void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

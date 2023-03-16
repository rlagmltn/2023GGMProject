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
    public Button quitButton;
}

/// <summary>
/// ��� �˾�â�� �����ϴ� �ֻ��� �Լ�
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
    /// ó�� �ʱ�ȭ ���ִ� �Լ�
    /// </summary>
    private void Init()
    {
        ButtonInit();
        BackGroundPanelActiveFalse();
    }
    
    /// <summary>
    /// ��ư �ʱ�ȭ
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

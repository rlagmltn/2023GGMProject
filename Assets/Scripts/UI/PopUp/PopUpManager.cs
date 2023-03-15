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
    /// ��׶��� �г��� ���ִ� ����
    /// </summary>
    void BackGroundPanelActiveTrue()
    {
        backgroundPanel.gameObject.SetActive(true);
    }

    /// <summary>
    /// ��׶��� �г��� ���ִ� ����
    /// </summary>
    void BackGroundPanelActiveFalse()
    {
        backgroundPanel.gameObject.SetActive(false);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceItem : _Event
{
    [SerializeField] Button OptionButton_1;
    [SerializeField] Button OptionButton_2;
    [SerializeField] Button OptionButton_3;

    [SerializeField] List<Button> CancelButtons;

    [SerializeField] Transform PopupPannel_1;
    [SerializeField] Transform PopupPannel_2;
    [SerializeField] Transform PopupPannel_3;

    [SerializeField] private Transform BackgroundPannel;

    public override void EventStart()
    {
        EventInit();
        ButtonInit();
    }

    void EventInit()
    {

    }

    void ButtonInit()
    {
        OptionButton_1.onClick.RemoveAllListeners();
        OptionButton_2.onClick.RemoveAllListeners();
        OptionButton_3.onClick.RemoveAllListeners();

        OptionButton_1.onClick.AddListener(OptionButtonClicked_1);
        OptionButton_2.onClick.AddListener(OptionButtonClicked_2);
        OptionButton_3.onClick.AddListener(OptionButtonClicked_3);

        for(int i = 0; i < CancelButtons.Count; i++)
        {
            CancelButtons[i].onClick.RemoveAllListeners();
            CancelButtons[i].onClick.AddListener(CancelButtonClicked);
        }
    }

    void OptionButtonClicked_1()
    {
        BackgroundPannel.gameObject.SetActive(true);
        PopupPannel_1.gameObject.SetActive(true);
    }

    void OptionButtonClicked_2()
    {
        BackgroundPannel.gameObject.SetActive(true);
        PopupPannel_2.gameObject.SetActive(true);
    }

    void OptionButtonClicked_3()
    {
        BackgroundPannel.gameObject.SetActive(true);
        PopupPannel_3.gameObject.SetActive(true);
    }

    void CancelButtonClicked()
    {
        BackgroundPannel.gameObject.SetActive(false);
        PopupPannel_1.gameObject.SetActive(false);
        PopupPannel_2.gameObject.SetActive(false);
        PopupPannel_3.gameObject.SetActive(false);
    }
}

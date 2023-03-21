using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StartPannel : MonoBehaviour
{
    [SerializeField] private Button EasyButton;
    [SerializeField] private Button NormalButton;
    [SerializeField] private Button HardButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ButtonInit();
    }

    private void ButtonInit()
    {
        RemoveAllEvents(EasyButton);
        RemoveAllEvents(NormalButton);
        RemoveAllEvents(HardButton);

        AddButtonEvents(EasyButton, EasyModeStart);
        AddButtonEvents(NormalButton, NormalModeStart);
        AddButtonEvents(HardButton, HardModeStart);
    }

    private void EasyModeStart()
    {
        Debug.Log("������ ����");
    }

    private void NormalModeStart()
    {
        Debug.Log("������ ����");
    }

    private void HardModeStart()
    {
        Debug.Log("������� ����");
    }

    private void RemoveAllEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    private void AddButtonEvents(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

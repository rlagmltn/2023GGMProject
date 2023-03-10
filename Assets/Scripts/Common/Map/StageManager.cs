using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private List<Button> stageButtons;

    [SerializeField] private Transform stagePanel;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI explanationText;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button closePanel;

    [SerializeField] private int stageProgress;

    private void Start()
    {
        AllStageButtonAddEvent();
    }

    void AllStageButtonAddEvent()
    {
        foreach(Button btn in stageButtons)
        {
            AddButtonEvent(btn, btn.GetComponent<StageHolder>().OnClick);
        }
        AddButtonEvent(closeButton, CloseStageInfo);
        AddButtonEvent(closePanel, CloseStageInfo);
    }

    public void ShowStageInfo(StageSO stage)
    {
        stagePanel.gameObject.SetActive(true);
        stageNameText.text = stage.stageName;
        explanationText.text = stage.explanationText;
    }

    void CloseStageInfo()
    {
        stagePanel.gameObject.SetActive(false);
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

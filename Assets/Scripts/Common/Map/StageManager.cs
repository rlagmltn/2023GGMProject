using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;
public class StageManager : MonoSingleton<StageManager>
{
    [SerializeField] private List<Button> stageButtons;
    [SerializeField] private StageSOList s_List;

    [SerializeField] private Transform stagePanel;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private Image MapImage;

    [SerializeField] private Button closeButton;
    [SerializeField] private Button startButton;

    private void Start()
    {
        AllStageButtonAddEvent();
    }

    void AllStageButtonAddEvent()
    {
        for(int num = 0; num < stageButtons.Count; num++)
        {
            stageButtons[num].GetComponent<StageHolder>().SetStageSO(s_List.stageList[num]);
            RemoveAllButtonEvents(stageButtons[num]);
            AddButtonEvent(stageButtons[num], stageButtons[num].GetComponent<StageHolder>().GetStageSO);
        }

        RemoveAllButtonEvents(closeButton);
        AddButtonEvent(closeButton, CloseStageInfo);
        RemoveAllButtonEvents(startButton);
        AddButtonEvent(startButton, StageStart);
    }

    public void ShowStageInfo(StageSO stage)
    {
        stagePanel.gameObject.SetActive(true);
        stageNameText.text = stage.stageName;
        explanationText.text = stage.explanationText;
        MapImage.sprite = stage.stageImage;
        Global.Map = stage.map;
    }

    void CloseStageInfo()
    {
        stagePanel.gameObject.SetActive(false);
    }

    void StageStart()
    {
        //MGScene.Instance.ChangeScene(eSceneName.InGame);
        SceneManager.LoadScene("InGameScene");
        Debug.Log("Change Scene to InGameScene");
    }

    void RemoveAllButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManager : MonoSingleton<StageUIManager>
{
    [SerializeField] private Transform StageInfoPannel;
    [SerializeField] private List<Transform> AllButtons;
    [SerializeField] private List<Transform> EnemyButtons;
    [SerializeField] private List<Transform> RewardButtons;
        
    [SerializeField] private Button CancelButton;
    [SerializeField] private Button StartButton;

    [SerializeField] private List<Transform> CharacterButtons;
    [SerializeField] private ArSOList ArList;

    private StageSO Selected_Stage;
    private List<ArSO> sortedArSO;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ButtonInit();
        ClassfyArSO();
    }

    void ButtonInit()
    {
        for (int num = 0; num < AllButtons.Count; num++)
        {
            AllButtons[num].GetComponent<Button>().onClick.RemoveAllListeners();
            AllButtons[num].GetComponent<Button>().onClick.AddListener(AllButtons[num].GetComponent<StageSOHolder>().StageSelect);
        }
        CancelButton.onClick.RemoveAllListeners();
        StartButton.onClick.RemoveAllListeners();

        CancelButton.onClick.AddListener(() => StageInfoPannelActive(false));
        StartButton.onClick.AddListener(StageManager.Instance.StageEnter);
    }

    public void SetSelectedStage(StageSO stage)
    {
        Selected_Stage = stage;
        StageInfoPannelActive(true);
        EnemyUI();
        RewardUI();
    }

    void StageInfoPannelActive(bool isActive)
    {
        StageInfoPannel.gameObject.SetActive(isActive);
    }

    private void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if (ArList.list[num].isInGameTake && !ArList.list[num].isDead) sortedArSO.Add(ArList.list[num]);

        UpdateStageInfoPannel_Scrollview();
    }

    void UpdateStageInfoPannel_Scrollview()
    {
        for(int num = 0; num < CharacterButtons.Count; num++)
        {
            if(num > sortedArSO.Count - 1)
            {
                CharacterButtons[num].GetComponent<ArSOHolder_Map>().SetArSO(null);
                continue;
            }
            CharacterButtons[num].GetComponent<ArSOHolder_Map>().SetArSO(sortedArSO[num]);
        }
    }

    void EnemyUI()
    {
        for(int num = 0; num < EnemyButtons.Count; num++)
        {
            EnemyButtons[num].gameObject.SetActive(true);

            if (num <= Selected_Stage.StageEnemy.Count - 1)
            {
                EnemyButtons[num].GetChild(0).GetComponent<Image>().sprite = Selected_Stage.StageEnemy[num].EnemyImage;
                continue;
            }

            EnemyButtons[num].gameObject.SetActive(false);
        }
    }

    void RewardUI()
    {
        for (int num = 0; num < RewardButtons.Count; num++)
        {
            RewardButtons[num].gameObject.SetActive(true);

            if (num <= Selected_Stage.stageRewardSprite.Count - 1)
            {
                RewardButtons[num].GetChild(0).GetComponent<Image>().sprite = Selected_Stage.stageRewardSprite[num];
                continue;
            }

            RewardButtons[num].gameObject.SetActive(false);
        }
    }

    private void OnApplicationQuit()
    {
        foreach(ArSO Ar in ArList.list)
        {
            Ar.isInGameTake = false;
            Ar.isDead = false;
        }
    }
}

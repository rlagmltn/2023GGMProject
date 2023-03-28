using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHolder : MonoBehaviour
{
    public StageSO Stage;

    public void OnClick()
    {
        
    }

    public void SetStageSO(StageSO stage)
    {
        Stage = stage;
    }

    public void GetStageSO()
    {
        //stageManager에 무언가를 보내서 실행시켜야할듯?
        StageManager.Instance.ShowStageInfo(Stage);
    }
}

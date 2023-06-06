using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatReset : MonoBehaviour
{
    [SerializeField] StageSOList[] stages;

    public void Resetting()
    {
        Global.EnterStage = null;
        foreach(StageSOList stage in stages)
        {
            foreach(StageSO sta in stage.stageList)
            {
                sta.IsCleared = false;
            }
        }
        MGUI.Instance.MoveToMainScene();
    }
}

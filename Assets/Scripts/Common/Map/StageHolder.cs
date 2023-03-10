using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageHolder : MonoBehaviour
{
    public StageSO stage;

    public void OnClick()
    {
        if (stage == null)
        {
            Debug.LogWarning("StageHolder의 stage비어있음");
            return;
        }

        StageManager.Instance.ShowStageInfo(stage);
    }
}

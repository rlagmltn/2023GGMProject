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
            Debug.LogWarning("StageHolder�� stage�������");
            return;
        }

        StageManager.Instance.ShowStageInfo(stage);
    }
}

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
        //stageManager�� ���𰡸� ������ ������Ѿ��ҵ�?
        StageManager.Instance.ShowStageInfo(Stage);
    }
}

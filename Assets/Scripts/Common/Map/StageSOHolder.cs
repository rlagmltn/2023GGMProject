using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSOHolder : MonoBehaviour
{
    [SerializeField] private StageSO Stage;

    public List<Transform> NextStageList;

    public StageSO GetStage()
    {
        return Stage;
    }

    public void EnterStage()
    {
        TestStageManager.Instance.StageEnter(Stage);
    }
}

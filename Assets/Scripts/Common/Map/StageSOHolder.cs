using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSOHolder : MonoBehaviour
{
    [SerializeField] private StageSO Stage;

    public List<Transform> NextStageList;

    public void ChangeImage()
    {
        Sprite TempSprite = StageImageChanger.Instance.GetStageImage(Stage.stageKind);

        transform.GetComponent<Image>().sprite = TempSprite;
    }

    public StageSO GetStage()
    {
        return Stage;
    }

    public void StageSelect()
    {
        TestStageManager.Instance.InfoPannelActiveTrue(Stage);
    }
}

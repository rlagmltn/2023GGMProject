using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade09 : MonoBehaviour
{
    public bool[,] UpgradeCheck = new bool[8, 3];
    
    public bool ViewInfo { get; private set; }
    private Upgrade09Btn[] btns;

    [SerializeField] private Sprite onimage;
    [SerializeField] private Sprite offimage;
    [SerializeField] private Image viewInfoBtn;

    private void Start()
    {
        btns = FindObjectsOfType<Upgrade09Btn>(true);
        ViewInfo = false;
        UpgradeCheck = SaveManager.Instance.PlayerData.UpgradeCheck;
    }

    public void Upgrade(int rank, int num)
    {
        UpgradeCheck[rank, num] = true;
        for (int i=0; i<btns.Length; i++)
        {
            btns[i].CheckBtnChange();
        }
        SaveManager.Instance.PlayerData.UpgradeCheck = UpgradeCheck;
        SaveManager.Instance.PlayerDataSave();
    }

    public bool CheckRankUpgraded(int rank)
    {
        if (rank < 0) return true;

        bool result = false;
        for(int i=0; i<3; i++)
        {
            if (UpgradeCheck[rank, i]) result = true;
        }

        return result;
    }

    public void ToggleViewInfo()
    {
        ViewInfo = !ViewInfo;

        if(ViewInfo)
        {
            viewInfoBtn.sprite = onimage;
        }
        else
        {
            viewInfoBtn.sprite = offimage;
        }
    }

    //여기에 이제 각 업그레이드가 적용되었을 때의 효과 코딩
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade09 : MonoSingleton<Upgrade09>
{
    [SerializeField] ArSOArray inven;
    [SerializeField] ArSOArray holder;

    public Bool2Arr[] UpgradeCheck = new Bool2Arr[8];

    public bool ViewInfo { get; private set; }
    private Upgrade09Btn[] btns;

    [SerializeField] private Sprite onimage;
    [SerializeField] private Sprite offimage;
    [SerializeField] private Image viewInfoBtn;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if(SaveManager.Instance.PlayerData.UpgradeCheck!=null) UpgradeCheck = SaveManager.Instance.PlayerData.UpgradeCheck;
        btns = FindObjectsOfType<Upgrade09Btn>(true);
        ViewInfo = false;
    }

    public void Upgrade(int rank, int num)
    {
        UpgradeCheck[rank].arr[num] = true;

        UpdateBtns();
         
        SaveManager.Instance.PlayerData.UpgradeCheck = UpgradeCheck;
        SaveManager.Instance.PlayerDataSave();
    }

    public void UpdateBtns()
    {
        for (int i = 0; i < btns.Length; i++)
        {
            btns[i].CheckBtnChange();
        }
    }

    public bool CheckRankUpgraded(int rank)
    {
        if (rank < 0) return true;

        bool result = false;
        for(int i=0; i<3; i++)
        {
            if (UpgradeCheck[rank].arr[i]) result = true;
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

    public void ActUpgrade(ArSO ar)
    {
        Rank0(ar);
        Rank1(ar);
        Rank2(ar);
        Rank3(ar);
        Rank4(ar);
        Rank5(ar);
        Rank6(ar);
        Rank7(ar);
    }

    private void Rank0(ArSO ar)
    {
        ar.attackStats.currentSkillAtk += 1;
    }

    private void Rank1(ArSO ar)
    {
        if(UpgradeCheck[1].arr[0])
        {
            ar.attackStats.currentAtk += 1;
        }
        else if(UpgradeCheck[1].arr[1])
        {
            ar.surviveStats.MaxHP += 3;
            ar.surviveStats.currentHP += 3;
        }
        else if(UpgradeCheck[1].arr[2])
        {
            ar.surviveStats.MaxShield += 2;
            ar.surviveStats.currentShield += 2;
        }
    }

    private void Rank2(ArSO ar)
    {
        if (UpgradeCheck[2].arr[0])
        {
            ar.surviveStats.currentWeight += 2;
        }
        else if (UpgradeCheck[2].arr[1])
        {
            ar.criticalStats.currentCriticalPer += 10;
        }
        else if (UpgradeCheck[2].arr[2])
        {
            ar.criticalStats.currentCriticalDamage += 15;
        }
    }

    private void Rank3(ArSO ar)
    {
        if (UpgradeCheck[3].arr[0])
        {
            ar.surviveStats.MaxShield += 3;
            ar.surviveStats.currentShield += 3;
        }
        else if (UpgradeCheck[3].arr[1])
        {
            ar.surviveStats.MaxHP += 5;
            ar.surviveStats.currentHP += 5;
        }
        else if (UpgradeCheck[3].arr[2])
        {
            ar.surviveStats.currentWeight += 3;
        }
    }

    private void Rank4(ArSO ar)
    {
        if (UpgradeCheck[4].arr[0])
        {
            ar.skill.MaxSkillCoolTime -= 1;
        }
    }

    private void Rank5(ArSO ar)
    {
        if (UpgradeCheck[5].arr[0])
        {
            ar.attackStats.currentSkillAtk += 1;
        }
        else if (UpgradeCheck[5].arr[1])
        {
            ar.criticalStats.currentCriticalPer += 15;
        }
        else if (UpgradeCheck[5].arr[2])
        {
            ar.attackStats.currentAtk += 2;
        }
    }

    private void Rank6(ArSO ar)
    {
        if (UpgradeCheck[6].arr[3])
        {
            ar.surviveStats.currentWeight -= 1;
        }
    }

    public int Rank6GoldUp(int gold)
    {
        if (UpgradeCheck[6].arr[0])
        {
            float nGold = gold * 1.3f;
            return (int)nGold;
        }
        else return gold;
    }

    public int Rank6ExpUp(int exp)
    {
        if (UpgradeCheck[6].arr[1])
        {
            float nExp = exp * 1.25f;
            return (int)nExp;
        }
        else return exp;
    }


    private void Rank7(ArSO ar)
    {
        if (UpgradeCheck[7].arr[0])
        {
            ar.skill.MaxSkillCoolTime -= 1;
        }
        else if (UpgradeCheck[7].arr[1])
        {
            ar.attackStats.currentSkillAtk += 2;
        }
        else if (UpgradeCheck[7].arr[2])
        {
            ar.criticalStats.currentCriticalPer += 20;
        }
    }
}

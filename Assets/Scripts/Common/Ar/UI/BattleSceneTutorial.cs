using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] battleBeforeTutos;
    [SerializeField] private GameObject[] battleTutos;
    [SerializeField] private GameObject[] sellectTutos;

    private void Start()
    {
        if (!SaveManager.Instance.TutoData.beforeBattleTuto) battleBeforeTutos[0].SetActive(true);
    }

    public void ActiveNextBeforeTuto(int i = 0)
    {
        battleBeforeTutos[i].SetActive(true);
        battleBeforeTutos[i - 1].SetActive(false);
    }

    public void FinalBeforeTuto()
    {
        SaveManager.Instance.TutoData.beforeBattleTuto = true;
        SaveManager.Instance.TutoDataSave();
        battleBeforeTutos[battleBeforeTutos.Length - 1].SetActive(false);
    }

    public void ActiveNextBattleTuto(int i = 0)
    {
        battleTutos[i].SetActive(true);
        battleTutos[i - 1].SetActive(false);
    }

    public void FinalBattleTuto()
    {
        SaveManager.Instance.TutoData.battleTuto = true;
        SaveManager.Instance.TutoDataSave();
        battleTutos[battleTutos.Length - 1].SetActive(false);
    }

    public void ActiveSellectTuto(int i = 0)
    {
        sellectTutos[i].SetActive(true);
        sellectTutos[i - 1].SetActive(false);
    }

    public void FinalSellectTuto()
    {
        SaveManager.Instance.TutoData.sellectTuto = true;
        SaveManager.Instance.TutoDataSave();
        sellectTutos[sellectTutos.Length - 1].SetActive(false);
    }
}

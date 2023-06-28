using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneTutorial : MonoBehaviour
{
    [SerializeField] private GameObject[] battleBeforeTutos;
    [SerializeField] private GameObject[] battleTutos;

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GoldReward : MonoBehaviour
{
    private int getCoin;
    [SerializeField] GameObject okBtn;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] GameObject WinPanel;
    [SerializeField] GameObject BossWinPanel;
    [SerializeField] GameObject warningPanel;

    private void OnEnable()
    {
        StartCoroutine(CoinCountUp());
    }

    private void Update()
    {
        if(Input.GetMouseButton(0))
        {
            StopAllCoroutines();
            tmp.SetText(getCoin.ToString());

            okBtn.SetActive(true);
            okBtn.transform.localScale = new Vector3(1, 1, 1);
            okBtn.transform.localPosition = new Vector3(0, -360, 0);
        }
    }

    private IEnumerator CoinCountUp()
    {
        okBtn.transform.localScale = new Vector2(0, 0);
        okBtn.SetActive(false);
        getCoin = Random.Range(50, 300);
        tmp.SetText("0");
        int coinText = 0;
        while(coinText<getCoin)
        {
            coinText++;
            tmp.SetText(coinText.ToString());
            yield return null;
        }
        okBtn.SetActive(true);
        okBtn.transform.DOScale(1, 0.6f);
        okBtn.transform.DOLocalMoveY(-360, 1);
    }

    public void ActiveNextPanel()
    {
        GoldManager.Instance.AddGold(getCoin);
        if(SpawnManager.Instance.battleMapSO.IsBossMap)
        {
            BossWinPanel.SetActive(true);
            TwinManager_Event.Instance.SetDAP(0.2f, BossWinPanel.transform.GetChild(1));
            TwinManager_Event.Instance.SetDAP(0.5f, BossWinPanel.transform.GetChild(2));
            TwinManager_Event.Instance.SetDAP(0.5f, BossWinPanel.transform.GetChild(0));
            TwinManager_Event.Instance.EventStart();
        }
        else
        {
            WinPanel.SetActive(true);
            TwinManager_Event.Instance.SetDAP(0.2f, WinPanel.transform.GetChild(1));
            TwinManager_Event.Instance.SetDAP(0.5f, WinPanel.transform.GetChild(2));
            TwinManager_Event.Instance.SetDAP(0.5f, WinPanel.transform.GetChild(0));
            TwinManager_Event.Instance.EventStart();
        }
        gameObject.SetActive(false);
    }

    public void SkipReward()
    {
        warningPanel.SetActive(true);
    }

    public void SkipAndNextPanel()
    {
        if (SpawnManager.Instance.battleMapSO.IsBossMap)
        {
            BossWinPanel.SetActive(true);
        }
        else
        {
            WinPanel.SetActive(true);
        }
        gameObject.SetActive(false);
    }

    public void SkipCancel()
    {
        warningPanel.SetActive(false);
    }
}

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
        WinPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

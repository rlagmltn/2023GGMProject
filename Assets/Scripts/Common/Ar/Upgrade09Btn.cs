using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade09Btn : MonoBehaviour
{
    [SerializeField] int rank;
    [SerializeField] int num;

    private Button btn;
    public Image image;
    public string info;
    private Upgrade09Text upgrade09Text;

    void Start()
    {
        upgrade09Text = FindObjectOfType<Upgrade09Text>();
        btn = GetComponent<Button>();
        image = GetComponent<Image>();

        btn.onClick.AddListener(ClickBtn);
        CheckBtnChange();
    }

    public void CheckBtnChange()
    {
        if (Upgrade09.Instance.UpgradeCheck[rank].arr[num])
        {
            btn.interactable = false;
            image.color = Color.green;
        }
        else if (Upgrade09.Instance.CheckRankUpgraded(rank - 1) && SaveManager.Instance.PlayerData.PlayerLevel > rank)
        {
            btn.interactable = true;
            image.color = Color.white;
        }
        else
        {
            btn.interactable = false;
            image.color = Color.gray;
        }

        if(Upgrade09.Instance.UpgradeCheck[rank].arr[num] && SaveManager.Instance.PlayerData.PlayerLevel <= rank)
        {
            btn.interactable = false;
            image.color = Color.gray;
            Upgrade09.Instance.UpgradeCheck[rank].arr[num] = false;
        }
    }

    private void ClickBtn()
    {
        if(Upgrade09.Instance.ViewInfo)
        {
            upgrade09Text.ShowText(this);
        }
        else
        {
            Upgrade();
        }
    }

    public void Upgrade()
    {
        for (int i = 0; i < 3; i++)
        {
            Upgrade09.Instance.UpgradeCheck[rank].arr[i] = false;
        }

        Upgrade09.Instance.Upgrade(rank, num);
    }
}

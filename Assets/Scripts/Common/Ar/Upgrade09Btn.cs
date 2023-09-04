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
    private Upgrade09 upgrade09;
    private Upgrade09Text upgrade09Text;

    void Start()
    {
        upgrade09 = FindObjectOfType<Upgrade09>();
        upgrade09Text = FindObjectOfType<Upgrade09Text>();
        btn = GetComponent<Button>();
        image = GetComponent<Image>();

        btn.onClick.AddListener(ClickBtn);
        CheckBtnChange();
    }

    public void CheckBtnChange()
    {
        if (upgrade09.UpgradeCheck[rank].arr[num])
        {
            btn.interactable = false;
            image.color = Color.green;
        }
        else if (upgrade09.CheckRankUpgraded(rank - 1) && SaveManager.Instance.PlayerData.PlayerLevel > rank)
        {
            btn.interactable = true;
            image.color = Color.white;
        }
        else
        {
            btn.interactable = false;
            image.color = Color.gray;
        }
    }

    private void ClickBtn()
    {
        if(upgrade09.ViewInfo)
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
            upgrade09.UpgradeCheck[rank].arr[i] = false;
        }

        upgrade09.Upgrade(rank, num);
    }
}

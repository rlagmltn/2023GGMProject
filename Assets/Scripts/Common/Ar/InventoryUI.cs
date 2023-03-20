using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventoryUI : MonoSingleton<InventoryUI>
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI atkText;
    [SerializeField] private TextMeshProUGUI HPText;
    [SerializeField] private TextMeshProUGUI weightText;
    [SerializeField] private TextMeshProUGUI explanationText;

    //public int Num;

    public ArSO Ar;

    public void UpdateUI()
    {
        if(Ar == ArInventorySelecter.Instance.emptyAr)
        {
            image.sprite = null;
            nameText.text = "";
            atkText.text = "";
            HPText.text = "";
            weightText.text = "";
            explanationText.text = "";
            return;
        }

        image.sprite = Ar.Image;
        nameText.text = "Name :  " + Ar.Name;
        atkText.text = "Atk : " + Ar.currentAtk;
        HPText.text = "HP : " + Ar.currentHP;
        weightText.text = "Weight : " + Ar.currentWeight;
        explanationText.text = Ar.Explanation;
    }

    public void UpdateEmpty()
    {
        Ar = null;
        UpdateUI();
    }
}

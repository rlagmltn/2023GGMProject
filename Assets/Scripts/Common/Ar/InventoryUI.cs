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
        image.sprite = Ar.Image;
        nameText.text = "Name :  " + Ar.Name;
        atkText.text = "Atk : " + Ar.currentAtk;
        HPText.text = "HP : " + Ar.currentHP;
        weightText.text = "Weight : " + Ar.currentWeight;
        explanationText.text = Ar.Explanation;
    }
}

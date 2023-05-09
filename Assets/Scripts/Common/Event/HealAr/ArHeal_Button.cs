using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArHeal_Button : MonoBehaviour
{
    [SerializeField] private Image CharacterImage;
    [SerializeField] private Image HPBar;
    [SerializeField] private TextMeshProUGUI HPText;

    private ArSO AR;

    private void UpdateUI()
    {
        CharacterImage.sprite = AR.characterInfo.Image;
        HPBar.fillAmount = AR.surviveStats.currentHP / AR.surviveStats.MaxHP;
        HPText.text = $"{AR.surviveStats.currentHP}/{AR.surviveStats.MaxHP}";
    }

    internal void ClickButton()
    {
        ArHeal_CharacterInventory.Instance.ClickButton();
        gameObject.GetComponent<Outline>().effectColor = new Color(1, 0, 0, 0.5f);
    }

    internal void SetAr(ArSO ar)
    {
        AR = ar;
        UpdateUI();
    }
}

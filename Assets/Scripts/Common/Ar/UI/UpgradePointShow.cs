using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradePointShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Image expImage;
    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        levelText.SetText(SaveManager.Instance.PlayerData.PlayerLevel.ToString());
        expImage.fillAmount = (float)SaveManager.Instance.PlayerData.PlayerExp / 500f;
    }
}

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
        SaveManager.Instance.PlayerData.PlayerLevel = SaveManager.Instance.PlayerData.PlayerExp / 500;
        levelText.SetText(SaveManager.Instance.PlayerData.PlayerLevel.ToString());
        expImage.fillAmount = ((float)SaveManager.Instance.PlayerData.PlayerExp % 500) / 500;
    }

    public void LevelUp()
    {
        SaveManager.Instance.PlayerData.PlayerExp += 500;
        UpdateText();
        Upgrade09.Instance.UpdateBtns();
    }

    public void LevelDown()
    {
        if (SaveManager.Instance.PlayerData.PlayerExp < 500) return;
        SaveManager.Instance.PlayerData.PlayerExp -= 500;
        UpdateText();
        Upgrade09.Instance.UpdateBtns();
    }
}

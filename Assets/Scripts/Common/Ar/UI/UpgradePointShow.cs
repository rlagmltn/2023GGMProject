using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePointShow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointText;
    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        pointText.SetText(SaveManager.Instance.PlayerData.UpgradePoint.ToString());
    }
}

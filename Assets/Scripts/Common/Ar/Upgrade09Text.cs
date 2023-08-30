using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade09Text : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI info;

    public void ShowText(Upgrade09Btn btn)
    {
        panel.SetActive(true);

        icon.sprite = btn.image.sprite;

        info.SetText(btn.info);
    }
}

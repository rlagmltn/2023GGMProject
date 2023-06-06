using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI GoldText;
    void Update()
    {
        GoldText.text = GoldManager.Gold.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    public Player Player { get; set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        outline = transform.GetChild(0).GetComponent<Image>();
    }

    public void Connect(Player player)
    {
        Player = player;
    }

    public void SellectPlayer()
    {
        PlayerController.Instance.SellectPlayer(this);
    }

    public void ColorChange(bool change)
    {
        if (change)
        {
            outline.color = Color.red;
        }
        else
            outline.color = Color.black;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    private Image playerImage;
    public Player Player { get; set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        playerImage = transform.GetChild(1).GetComponent<Image>();
        outline = transform.GetChild(0).GetComponent<Image>();

        playerImage.sprite = Player.arSprite;
    }

    public void Connect(Player player)
    {
        Player = player;
        Player.Connect(this);
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

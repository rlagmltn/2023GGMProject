using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    private Image playerImage;
    private Image unableImage;
    public Player Player { get; set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        playerImage = transform.GetChild(1).GetComponent<Image>();
        outline = transform.GetChild(0).GetComponent<Image>();
        unableImage = transform.GetChild(2).GetComponent<Image>();

        unableImage.gameObject.SetActive(false);
        playerImage.sprite = Player.ar_sprite;
    }

    public void Connect(Player player)
    {
        Player = player;
        Player.Connect(this);
    }

    public void SellectPlayer()
    {
        if (!TurnManager.Instance.IsPlayerTurn || Player.isDead || TurnManager.Instance.SomeoneIsMoving) return;
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

    public void SetSlotActive(bool value)
    {
        unableImage.gameObject.SetActive(!value);
    }
}

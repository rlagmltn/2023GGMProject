using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    private Image background;
    private Image playerImage;
    private Image unableImage;
    private Image hpImage;
    private Image spImage;

    public Player Player { get; set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        outline = transform.GetChild(0).GetComponent<Image>();
        background = transform.GetChild(1).GetComponent<Image>();
        playerImage = transform.GetChild(2).GetComponent<Image>();
        unableImage = transform.GetChild(3).GetComponent<Image>();
        hpImage = transform.GetChild(4).GetComponent<Image>();
        spImage = transform.GetChild(5).GetComponent<Image>();

        unableImage.gameObject.SetActive(false);
        playerImage.sprite = Player.so.characterInfo.Image;
    }

    public void Connect(Player player)
    {
        Player = player;
        Start();
        Player.Connect(this);
    }

    public void SellectPlayer()
    {
        if (!TurnManager.Instance.IsPlayerTurn || Player.isDead || TurnManager.Instance.SomeoneIsMoving || TurnManager.Instance.IsWaitingTurn) return;
        PlayerController.Instance.SellectPlayer(this);
    }

    public void ColorChange(bool change)
    {
        if (change)
        {
            background.color = Color.yellow;
        }
        else
            background.color = Color.black;
    }

    public void SkillReady(bool isReady)
    {
        if (isReady)
            outline.color = Color.yellow;
        else
            outline.color = Color.black;
    }

    public void SetSlotActive(bool value)
    {
        unableImage.gameObject.SetActive(!value);
    }

    public void SetHPBar(float value)
    {
        hpImage.fillAmount = value;
    }
    public void SetSPBar(float value)
    {
        spImage.fillAmount = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private bool isBatched;
    private JoyStick joyStick;
    private PlayerController playerController;

    public Player Player { get; set; }

    private void Init()
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
        joyStick = FindObjectOfType<JoyStick>();
        playerController = FindObjectOfType<PlayerController>();
        isBatched = false;
    }

    public void Connect(Player player)
    {
        Player = player;
        Init();
        Player.Connect(this);
    }

    public void SellectPlayer()
    {
        if (!TurnManager.Instance.IsPlayerTurn || Player.isDead || TurnManager.Instance.SomeoneIsMoving 
            || TurnManager.Instance.IsWaitingTurn || joyStick.isDraging) return;

        playerController.SellectPlayer(this);
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

    public void Click()
    {
        if (!playerController.IsBatchMode || Player.isDead) return;
        if (!isBatched && playerController.BatchCount >= 3) return;
        Player.gameObject.SetActive(true);
        Player.Collide.enabled = false;
        Player.transform.position = transform.position;
        background.color = Color.yellow;
    }

    public void Drag()
    {
        if (!playerController.IsBatchMode) return;
        Player.transform.position = Util.Instance.mousePosition;
    }

    public void Up()
    {
        if (!playerController.IsBatchMode) return;
        RaycastHit2D ray = Physics2D.Raycast(Player.transform.position, new Vector3(0, -1, 0), 0.01f, LayerMask.GetMask("Batch"));
        if (ray.collider != null && !ray.collider.CompareTag("UI"))
        {
            if (!isBatched)
            {
                playerController.BatchCount++;
                playerController.ChangeBatchCount();
                playerController.quickSlotHolder.Add(this);
            }

            isBatched = true;
            Player.Collide.enabled = true;
        }
        else
        {
            if (isBatched)
            {
                playerController.BatchCount--;
                playerController.ChangeBatchCount();
                playerController.quickSlotHolder.Remove(this);
            }

            isBatched = false;
            Player.gameObject.SetActive(false);
        }
        background.color = Color.black;
    }
}

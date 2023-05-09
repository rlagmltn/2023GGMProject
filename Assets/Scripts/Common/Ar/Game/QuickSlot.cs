using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    public Image rarityBackGround;
    public Image background;
    private Image playerImage;
    private Image unableImage;
    private Image hpImage;
    private Image spImage;

    private GameObject MoveIcon;
    private GameObject AttackIcon;
    private GameObject SkillIcon;

    public bool isBatched;
    private JoyStick joyStick;
    private PlayerController playerController;

    public Player Player { get; set; }

    private void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        outline = transform.GetChild(0).GetComponent<Image>();
        rarityBackGround = transform.GetChild(2).GetComponent<Image>();
        background = transform.GetChild(3).GetComponent<Image>();
        playerImage = transform.GetChild(4).GetComponent<Image>();
        hpImage = transform.GetChild(5).GetChild(0).GetComponent<Image>();
        spImage = transform.GetChild(6).GetChild(0).GetComponent<Image>();
        unableImage = transform.GetChild(7).GetComponent<Image>();
        MoveIcon = transform.GetChild(8).gameObject;
        AttackIcon = transform.GetChild(9).gameObject;
        SkillIcon = transform.GetChild(10).gameObject;

        unableImage.gameObject.SetActive(false);
        rarityBackGround.sprite = BackGroundHolder.Instance.BackGround(Player.so.characterInfo.rarity);
        playerImage.sprite = Player.so.characterInfo.Image;
        joyStick = FindObjectOfType<JoyStick>();
        playerController = FindObjectOfType<PlayerController>();
        isBatched = false;
        UnActiveIcons();
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
            outline.color = Color.white;
        }
        else
        {
            outline.color = Color.black;
            UnActiveIcons();
        }
    }

    public void SkillReady(float max, float current)
    {
        if (unableImage.gameObject.activeSelf)
        {
            background.fillAmount = 0;
            return;
        }

        background.fillAmount = current / max;

        if(background.fillAmount < 1)
        {
            background.color = Color.gray;
        }
        else
        {
            background.color = Color.yellow;
        }    
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
        outline.color = Color.white;
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
            outline.color = Color.black;
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
            outline.color = Color.black;
        }
    }

    public void ActiveIcon(JoystickType joystickType)
    {
        UnActiveIcons();
        GameObject icon = joystickType switch
        {
            JoystickType.Move => MoveIcon,
            JoystickType.Attack => AttackIcon,
            JoystickType.Skill => SkillIcon,
            _ => null,
        };
        icon.SetActive(true);
    }

    private void UnActiveIcons()
    {
        MoveIcon.SetActive(false);
        AttackIcon.SetActive(false);
        SkillIcon.SetActive(false);
    }
}

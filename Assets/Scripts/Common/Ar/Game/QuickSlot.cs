using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    private Image outline;
    public Image rarityBackGround;
    public Image background;
    private Image playerImage;
    private Image unableImage;
    private Bar hpImage;
    private Bar spImage;

    private GameObject MoveIcon;
    private GameObject AttackIcon;
    private GameObject SkillIcon;

    public bool isBatched;
    private JoyStick joyStick;
    private PlayerController playerController;
    private bool isClicked;
    private bool isUnabled;

    public Player Player { get; set; }

    private Color unableColor = new Color(0, 0, 0, 0.7f);
    private Color enableColor = new Color(0, 0, 0, 0);

    private void Init()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
        MoveIcon = transform.GetChild(0).gameObject;
        AttackIcon = transform.GetChild(1).gameObject;
        SkillIcon = transform.GetChild(2).gameObject;
        outline = transform.GetChild(3).GetComponent<Image>();
        rarityBackGround = transform.GetChild(4).GetComponent<Image>();
        background = transform.GetChild(5).GetComponent<Image>();
        playerImage = transform.GetChild(6).GetComponent<Image>();
        hpImage = transform.GetChild(7).GetComponent<Bar>();
        spImage = transform.GetChild(8).GetComponent<Bar>();
        unableImage = transform.GetChild(9).GetComponent<Image>();

        isUnabled = false;
        unableImage.color = enableColor;
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
        if (!change)
        {
            UnActiveIcons();
        }
    }

    public void SkillReady(float max, float current)
    {
        if (isUnabled)
        {
            background.fillAmount = 0;
            return;
        }

        background.fillAmount = current / max;

        if(background.fillAmount < 1)
        {
            background.color = Color.gray - new Color(0, 0, 0, 0.4f);
        }
        else
        {
            background.color = Color.yellow - new Color(0, 0, 0, 0.4f);
        }    
    }

    public void SetSlotActive(bool value)
    {
        if(value)
        {
            isUnabled = false;
            unableImage.DOColor(enableColor, 0.2f);
        }
        else
        {
            isUnabled = true;
            unableImage.DOColor(unableColor, 0.2f);
        }
    }

    public void SetHPBar(float value)
    {
        hpImage.GageChange(value);
    }
    public void SetSPBar(float value)
    {
        spImage.GageChange(value);
    }

    public void Click()
    {
        if (!playerController.IsBatchMode || Player.isDead) return;
        if (!isBatched && playerController.BatchCount >= 3) return;
        Player.gameObject.SetActive(true);
        Player.Collide.enabled = false;
        Player.transform.position = transform.position;
        isClicked = true;
    }

    public void Drag()
    {
        if (!playerController.IsBatchMode || !isClicked) return;
        Player.transform.position = Util.Instance.mousePosition;
    }

    public void Up()
    {
        if (!playerController.IsBatchMode || !isClicked) return;
        RaycastHit2D ray = Physics2D.Raycast(Player.transform.position, new Vector3(0, -1, 0.1f), 0.01f, LayerMask.GetMask("Batch"));
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
            EffectManager.Instance.InstantiateEffect_P(Effect.LandingSmoke, Player.transform.position - new Vector3(0, 0.3f, 0));
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
        isClicked = false;
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
        icon.transform.DOLocalMoveX(207, 0.3f);
    }

    private void UnActiveIcons()
    {
        MoveIcon.transform.DOLocalMoveX(0, 0.3f);
        AttackIcon.transform.DOLocalMoveX(0, 0.3f);
        SkillIcon.transform.DOLocalMoveX(0, 0.3f);
    }
}

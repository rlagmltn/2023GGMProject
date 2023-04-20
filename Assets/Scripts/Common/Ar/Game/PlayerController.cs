using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int playerCount;
    [SerializeField] QuickSlot pf_QuickSlot;
    [SerializeField] Transform slotHolder;
    [SerializeField] Transform batchHolderTrs;
    [SerializeField] GameObject batchUI;
    [SerializeField] GameObject batchZone;
    [SerializeField] JoyStick joystick;
    [SerializeField] StickCancel cancelButton;
    [SerializeField] GameObject actSellect;
    [SerializeField] TextMeshProUGUI batchUnitText;

    public List<CONEntity> enemyList;

    public Player sellectPlayer = null;

    public bool IsBatchMode { get; set; }
    public int BatchCount { get; set; }

    private List<QuickSlot> quickSlots = new List<QuickSlot>();
    public List<QuickSlot> quickSlotHolder = new List<QuickSlot>();
    private GameObject attackBtn;
    private GameObject skilCoolImage;
    private TextMeshProUGUI skillCoolText;
    private CameraMove cameraMove;

    void Awake()
    {
        IsBatchMode = true;
        SummonPlayers();
        attackBtn = actSellect.transform.GetChild(1).gameObject;
        skilCoolImage = actSellect.transform.GetChild(2).GetChild(1).gameObject;
        skillCoolText = skilCoolImage.GetComponentInChildren<TextMeshProUGUI>();
        cameraMove = FindObjectOfType<CameraMove>();
    }

    //private void SetEnemyList()
    //{
    //    foreach(CONEntity con in MGPool.Instance.poolListDic[ePrefabs.Enemy])
    //    {
    //        enemyList.Add(con);
    //    }
    //}

    private void SummonPlayers()
    {
        for (int i = 0; i < ArInventoryManager.Instance.InvenList.Count; i++)
        {
            var player = Instantiate(ArInventoryManager.Instance.InvenList[i].ArData, null);
            var quickSlot = Instantiate(pf_QuickSlot, batchHolderTrs);
            quickSlot.Connect(player);
        }
        joystick.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        ChangeBatchCount();
    }

    public void SellectPlayer(QuickSlot player)
    {
        if (IsBatchMode) return;
        sellectPlayer = player.Player;
        cameraMove.MovetoTarget(sellectPlayer.transform);
        DisableQuickSlots();
        actSellect.SetActive(true);
        if (sellectPlayer.isRangeCharacter)
            attackBtn.SetActive(true);
        SetSkillBtnText();
        player.ColorChange(true);
    }

    public void DisableQuickSlots()
    {
        if (IsBatchMode) return;
        attackBtn.SetActive(false);
        actSellect.SetActive(false);
        joystick.gameObject.SetActive(false);
        foreach (QuickSlot slot in quickSlots)
        {
            slot.ColorChange(false);
        }
    }

    public void DragBegin(JoystickType joystickType)
    {
        if (IsBatchMode) return;
        cancelButton.gameObject.SetActive(true);
        sellectPlayer?.DragBegin(joystickType);
        TurnManager.Instance.BlinkNextTurn();
    }
    public void Drag(float angle, float dis)
    {
        if (IsBatchMode) return;
        sellectPlayer?.Drag(angle, dis);
    }

    public void DragEnd(float power, Vector2 angle)
    {
        if (IsBatchMode) return;
        TurnManager.Instance.StopBlink();
        if (cancelButton.entering)
        {
            sellectPlayer.DisableRanges();
            cancelButton.gameObject.SetActive(false);
            cancelButton.entering = false;
            return;
        }

        StartCoroutine(DragEnd_Couroutine(power, angle));
    }

    private IEnumerator DragEnd_Couroutine(float power, Vector2 angle)
    {
        StartCoroutine(sellectPlayer.DisableRanges_T());

        if (joystick.joystickType != JoystickType.Move)
        {
            while (true)
            {
                if (sellectPlayer.isEnd) break;

                yield return null;
                continue;
            }
        }

        if (TurnManager.Instance.UseTurn())
        {
            sellectPlayer.DragEnd(joystick.joystickType, power, angle);
            cancelButton.gameObject.SetActive(false);
            ResetSellect();

            foreach (QuickSlot quickSlot in quickSlots) quickSlot.Player.CountCooltime();
        }
    }

    public void ResetSellect()
    {
        sellectPlayer = null;
        DisableQuickSlots();
    }

    public void ChooseStickType(int n)
    {
        if ((JoystickType)n == JoystickType.Skill && sellectPlayer.currentCooltime > 0) return;

        attackBtn.SetActive(false);
        actSellect.SetActive(false);
        joystick.gameObject.SetActive(true);
        joystick.joystickType = (JoystickType)n;
    }

    private void SetSkillBtnText()
    {
        if (sellectPlayer.currentCooltime > 0)
        {
            skilCoolImage.SetActive(true);
            skillCoolText.SetText(sellectPlayer.currentCooltime.ToString());
        }
        else
        {
            skilCoolImage.SetActive(false);
            skillCoolText.SetText("");
        }
    }

    public void SetQuickSlotsEnable(bool value)
    {
        foreach (QuickSlot slot in quickSlots)
        {
            if (slot.Player.isDead) continue;

            slot.SetSlotActive(value);
        }
    }

    public void BattleStart()
    {
        if (BatchCount > playerCount || BatchCount <= 0) return;
        IsBatchMode = false;

        foreach(QuickSlot slot in quickSlotHolder)
        {
            quickSlots.Add(slot);
            slot.transform.SetParent(slotHolder);
        }
        batchZone.SetActive(false);
        batchUI.SetActive(false);

        GameManager.Instance.Finding();
    }

    public void ChangeBatchCount()
    {
        batchUnitText.SetText($"{BatchCount} / {playerCount}");

        if(BatchCount<=0)
        {
            batchUnitText.color = Color.red;
        }
        else if (BatchCount <= playerCount)
        {
            batchUnitText.color = Color.green;
        }
        else
        {
            batchUnitText.color = Color.red;
        }
    }
}

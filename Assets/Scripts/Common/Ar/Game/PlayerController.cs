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
    [SerializeField] TextMeshProUGUI batchUnitText;

    public List<CONEntity> enemyList;

    public Player sellectPlayer = null;

    public bool IsBatchMode { get; set; }
    public int BatchCount { get; set; }

    private List<QuickSlot> quickSlots = new List<QuickSlot>();
    public List<QuickSlot> quickSlotHolder = new List<QuickSlot>();
    private CameraMove cameraMove;

    void Awake()
    {
        IsBatchMode = true;
        cameraMove = FindObjectOfType<CameraMove>();
        SummonPlayers();
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
            //charactorDirection.Init(player);
            var quickSlot = Instantiate(pf_QuickSlot, batchHolderTrs);
            quickSlot.Connect(player);
        }
        joystick.Started();
        joystick.gameObject.SetActive(false);
        ChangeBatchCount();
    }

    public void SellectPlayer(QuickSlot player)
    {
        if (IsBatchMode) return;
        joystick.joystickType = JoystickType.None;
        sellectPlayer = player.Player;
        cameraMove.MovetoTarget(sellectPlayer);
        DisableQuickSlots();
        cameraMove.SetSkillBtnText(sellectPlayer);
        player.ColorChange(true);
    }

    public void DisableQuickSlots()
    {
        if (IsBatchMode) return;
        joystick.gameObject.SetActive(false);
        foreach (QuickSlot slot in quickSlots)
        {
            slot.ColorChange(false);
        }
    }

    public void DragBegin(JoystickType joystickType)
    {
        if (IsBatchMode) return;
        sellectPlayer?.DragBegin(joystickType);
        TurnManager.Instance.BlinkNextTurn();
    }
    public void Drag(float angle, float dis)
    {
        if (IsBatchMode) return;
        sellectPlayer?.Drag(angle, dis);
    }

    public void DragEnd(float power, Vector2 angle, bool canShoot = true)
    {
        if (IsBatchMode) return;
        TurnManager.Instance.StopBlink();

        if (!canShoot) return;
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
            ResetSellect();
            foreach (QuickSlot quickSlot in quickSlots) quickSlot.Player.CountCooltime();
        }
    }

    public void ResetSellect()
    {
        sellectPlayer = null;
        joystick.joystickType = JoystickType.None;
        DisableQuickSlots();
    }

    public void ChooseStickType(int n)
    {
        if ((JoystickType)n == JoystickType.Skill && sellectPlayer.currentCooltime < sellectPlayer.skillCooltime) return;

        cameraMove.CloseActSellect();
        joystick.joystickType = (JoystickType)n;
        sellectPlayer.slot.ActiveIcon(joystick.joystickType);
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

        DisableQuickSlots();
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

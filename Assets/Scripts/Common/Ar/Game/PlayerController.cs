using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] ArInventoryObj deck;
    [SerializeField] QuickSlot pf_QuickSlot;
    [SerializeField] Transform slotHolder;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] JoyStick joystick;
    [SerializeField] StickCancel cancelButton;
    [SerializeField] GameObject actSellect;

    public List<CONEntity> enemyList;

    public Player sellectPlayer = null;

    private List<QuickSlot> quickSlots = new List<QuickSlot>();
    private GameObject attackBtn;
    private TextMeshProUGUI skillBtnText;
    void Awake()
    {
        SummonPlayers();
        attackBtn = actSellect.transform.GetChild(1).gameObject;
        skillBtnText = actSellect.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>();
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
        for(int i=0; i<deck.inventorySlots.Length; i++)
        {
            var player = Instantiate(deck.inventorySlots[i].item, null);
            player.transform.position = spawnPoints[i].position;
            var quickSlot = Instantiate(pf_QuickSlot, slotHolder);
            quickSlots.Add(quickSlot);

            quickSlot.Connect(player);
        }
    }

    public void SellectPlayer(QuickSlot player)
    {
        sellectPlayer = player.Player;
        CameraMove.Instance.MovetoTarget(sellectPlayer);
        DisableQuickSlots();
        actSellect.SetActive(true);
        if (sellectPlayer.isRangeCharacter)
            attackBtn.SetActive(true);
        SetSkillBtnText();
        player.ColorChange(true);
    }

    public void DisableQuickSlots()
    {
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
        cancelButton.gameObject.SetActive(true);
        sellectPlayer?.DragBegin(joystickType);
    }
    public void Drag(float angle)
    {
        sellectPlayer?.Drag(angle);
    }

    //public void DragEnd(float power, Vector2 angle)
    //{
    //    StartCoroutine(sellectPlayer.DisableRanges_T());

    //    while(!sellectPlayer.isEnd)
    //    {
    //        Debug.Log("기다리는중");
    //    }

    //    if (cancelButton.entering)
    //    {
    //        cancelButton.entering = false;
    //        cancelButton.gameObject.SetActive(false);
    //        return;
    //    }
    //    else if (TurnManager.Instance.UseTurn())
    //    {
    //        sellectPlayer.DragEnd(joystick.joystickType, power, angle);
    //        cancelButton.gameObject.SetActive(false);
    //        ResetSellect();
            
    //        foreach(QuickSlot quickSlot in quickSlots)
    //        {
    //            quickSlot.Player.CountCooltime();
    //        }
    //    }
    //}

    public void DragEnd(float power,Vector2 angle)
    {
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

        while (true)
        {
            if (sellectPlayer.isEnd) break;

            yield return new WaitForSeconds(0.01f);
            continue;
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
            skillBtnText.SetText(sellectPlayer.currentCooltime.ToString());
        else
            skillBtnText.SetText("Skill");
    }
}

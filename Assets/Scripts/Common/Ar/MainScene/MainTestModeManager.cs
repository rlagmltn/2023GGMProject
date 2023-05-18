using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTestModeManager : MonoSingleton<MainTestModeManager>
{
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Enemy pfDummy;
    [SerializeField] private ArSOArray arHolder;
    [SerializeField] private ItemDBSO itemDB;
    [SerializeField] private ItemDBSO itemInven;
    [SerializeField] private ItemSO emptyItem;
    [SerializeField] private MainTestSlot pfMainTestSlot;
    [SerializeField] private Transform testBtnSlot;
    [SerializeField] private Transform testBtnSlot2;
    [SerializeField] private Transform BackgroundPannel;

    [SerializeField] MainTestJoyStick joystick;
    [SerializeField] GameObject actSellect;
    [SerializeField] MainTestSlot sellectPlayer;
    [SerializeField] MainTestSlot[] armedItems;

    [SerializeField] TextMeshProUGUI[] statTexts;

    private GameObject attackAct;
    private Transform testBtnSlotParent;
    private Player testPlayer;
    private Stat testPleyerStat;
    private Enemy dummy;
    public Player TestPlayer { get { return testPlayer; } }

    private void Start()
    {
        testBtnSlotParent = testBtnSlot.parent.parent.parent;
        MakeArTestSlot();
        joystick.gameObject.SetActive(false);
        attackAct = actSellect.transform.GetChild(1).gameObject;
        actSellect.SetActive(false);
        dummy = Instantiate(pfDummy, new Vector3(5, 0), Quaternion.identity);
    }

    private void MakeArTestSlot()
    {
        foreach(ArSO so in arHolder.list)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot);
            instance.SetSO(so);
            so.E_Item.itmeSO = new ItemSO[3];
        }
        foreach (ItemSO so in itemDB.items)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot2);
            instance.SetSO(so);
        }
        itemInven.items = new List<ItemSO>();
        for(int i=0; i<20; i++)
        {
            itemInven.AddItemInItems(emptyItem);
        }
    }

    //public void ToggleSellectArPanel()
    //{
    //    testBtnSlotParent.gameObject.SetActive(!testBtnSlotParent.gameObject.activeSelf);
    //    if(testBtnSlotParent.gameObject.activeSelf) sellectPlayer.UnSetAr();
    //}

    public void SellectPlayer(Player player = null)
    {
        if(testPlayer!=null)
        {
            testPlayer.UnArmed();
            for (int i = 0; i < 3; i++) armedItems[i].UnSetItem();
            testPlayer.so.E_Item.itmeSO = new ItemSO[3];
            testPlayer.isMainScene = false;
            testPlayer.gameObject.SetActive(false);
        }
        testPlayer = player;
        if (player != null)
        {
            sellectPlayer.GetAr(player.so);
            testPleyerStat = testPlayer.stat;
        }
        else testPleyerStat = new Stat();
        UpdateStatText();
    }

    public void ArmedItem(ItemSO item)
    {
        if (testPlayer == null) return;
        foreach(MainTestSlot slot in armedItems)
        {
            if (slot.itemSO == null)
            {
                slot.GetItem(item);
                testPleyerStat += item.stat;
                UpdateStatText();
                break;
            }
        }
    }

    public void UnArmedItem(ItemSO item)
    {
        testPleyerStat -= item.stat;
        UpdateStatText();
    }

    public void SummonPlayer()
    {
        testPlayer.gameObject.SetActive(true);
        testPlayer.transform.position = Vector3.zero;
        testPlayer.isMainScene = true;
        testBtnSlotParent.gameObject.SetActive(false);
        for (int i = 0; i < 3; i++) testPlayer.so.E_Item.itmeSO[i] = armedItems[i].itemSO;
        testPlayer.GetItemEvents();
        testPlayer.StatReset();
        joystick.joystickType = JoystickType.None;
        joystick.gameObject.SetActive(false);
        actSellect.SetActive(true);
        attackAct.SetActive(testPlayer.isRangeCharacter);
        cameraMove.MovetoTarget(testPlayer);
        dummy.StatReset();
        dummy.gameObject.SetActive(true);
        dummy.transform.position = new Vector3(5, 0);
        cameraMove.SetSkillBtnText(testPlayer);
        TurnManager.Instance.SomeoneIsMoving = false;
        BackgroundPannel.gameObject.SetActive(false);
    }

    public void DragBegin()
    {
        cameraMove.MovetoTarget(testPlayer.transform);
        testPlayer?.DragBegin(joystick.joystickType);
    }

    public void Drag(float angle, float dis)
    {
        testPlayer?.Drag(angle, dis);
    }

    public void DragEnd(float power, Vector2 angle, bool canShoot = true)
    {
        testPlayer.DisableRanges();
        if (!canShoot) return;
        testPlayer.DragEnd(joystick.joystickType, power, angle);
        testPlayer.CountCooltime();
        cameraMove.SetSkillBtnText(testPlayer);
    }

    public void ChooseStickType(int n)
    {
        if (testPlayer == null) return;
        if ((JoystickType)n == JoystickType.Skill && testPlayer.currentCooltime < testPlayer.skillCooltime) return;

        cameraMove.MovetoTarget(testPlayer.transform);
        joystick.joystickType = (JoystickType)n;
    }

    private void UpdateStatText()
    {
        statTexts[0].SetText(testPleyerStat.MaxHP.ToString());
        statTexts[1].SetText(testPleyerStat.MaxSP.ToString());
        statTexts[2].SetText(testPleyerStat.ATK.ToString());
        statTexts[3].SetText(testPleyerStat.SATK.ToString());
        statTexts[4].SetText(testPleyerStat.CriPer.ToString());
        statTexts[5].SetText(testPleyerStat.CriDmg.ToString());
        statTexts[6].SetText(testPleyerStat.WEIGHT.ToString());
    }
}

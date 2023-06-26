using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainTestModeManager : MonoSingleton<MainTestModeManager>
{
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Enemy pfDummy;
    [SerializeField] private ArSOList arHolder;
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
    private Stat testPlayerStat = new Stat();
    private Enemy dummy;
    private MainTestSlot slot = null;

    public Player TestPlayer { get { return testPlayer; } }

    private void Start()
    {
        testBtnSlotParent = testBtnSlot.parent.parent.parent;
        joystick.gameObject.SetActive(false);
        attackAct = actSellect.transform.GetChild(1).gameObject;
        actSellect.SetActive(false);
        dummy = Instantiate(pfDummy, new Vector3(5, 0), Quaternion.identity);
        SoundManager.Instance.Play(SoundManager.Instance.GetOrAddAudioClips("BackGroundMusic/Main", Sound.BGM), Sound.BGM);
        MakeArTestSlot();
        GameResult();

        slot.SellectPlayer();
        Invoke("SummonPlayer", 0.005f);
    }

    private void MakeArTestSlot()
    {
        foreach (ArSO so in arHolder.list)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot);
            instance.SetSO(so);
            so.E_Item.itmeSO = new ItemSO[3];
            if (slot == null) slot = instance;
        }
        foreach (ItemSO so in itemDB.items)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot2);
            instance.SetSO(so);
        }
        if (SaveManager.Instance.GameData.IsPlayingGame) return;
        ItemInvenReset();
    }

    public void ItemInvenReset()
    {
        itemInven.items = new List<ItemSO>();
        for (int i = 0; i < 21; i++)
        {
            itemInven.ResetInven(emptyItem);
        }
    }

    private void LateUpdate()
    {
        if (testPlayer.currentCooltime<testPlayer.skillCooltime)
            testPlayer?.CountCooltime();
    }

    public void SellectPlayer(Player player = null)
    {
        if (testPlayer != null)
        {
            testPlayer.UnArmed();
            for (int i = 0; i < 3; i++) armedItems[i].UnSetItem();
        }

        if (player != null)
        {
            if (testPlayer != null)
            {
                testPlayer.isMainScene = false;
                testPlayer.gameObject.SetActive(false);
            }
            testPlayer = player;
            sellectPlayer.GetAr(player.so);
            testPlayerStat = player.stat;
        }
        UpdateStatText();
    }

    public void ArmedItem(ItemSO item)
    {
        if (testPlayer == null) return;
        foreach (MainTestSlot slot in armedItems)
        {
            if (slot.itemSO == null)
            {
                slot.GetItem(item);
                testPlayerStat += item.stat;
                UpdateStatText();
                break;
            }
        }
    }

    public void UnArmedItem(ItemSO item)
    {
        testPlayerStat -= item.stat;
        testPlayer.UnActiveEvent();
        UpdateStatText();
    }

    public void SummonPlayer()
    {
        if (testPlayerStat.HP <= 0) return;
        testPlayer.gameObject.SetActive(true);
        testPlayer.transform.position = Vector3.zero;
        testPlayer.isMainScene = true;
        var count = testPlayer.transform.childCount;
        while (count > 5)
        {
            Destroy(testPlayer.transform.GetChild(--count).gameObject);
        }
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
        if (testPlayer == null) return;
        statTexts[0].SetText(testPlayerStat.MaxHP.ToString());
        statTexts[1].SetText(testPlayerStat.MaxSP.ToString());
        statTexts[2].SetText(testPlayerStat.ATK.ToString());
        statTexts[3].SetText(testPlayerStat.SATK.ToString());
        statTexts[4].SetText(testPlayerStat.CriPer.ToString());
        statTexts[5].SetText(testPlayerStat.CriDmg.ToString());
        statTexts[6].SetText(testPlayerStat.WEIGHT.ToString());
    }

    private void GameResult()
    {
        if (SaveManager.Instance.GameData.IsPlayingGame) return;
        //여기에 남은 골드와 돌파한 스테이지의 수에 따라서 업그레이드포인트를 준다.
        var gamedata = SaveManager.Instance.GameData;
        gamedata.PointPoint += gamedata.Gold;

        SaveManager.Instance.PlayerData.UpgradePoint += gamedata.PointPoint / 100;

        GoldManager.Instance.ResetGold();
        gamedata.PointPoint = 0;
        gamedata.ClearStages = 0;
        SaveManager.Instance.GameDataSave();
        SaveManager.Instance.PlayerDataSave();
    }
}
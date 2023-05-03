using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTestModeManager : MonoSingleton<MainTestModeManager>
{
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Enemy pfDummy;
    [SerializeField] private ArSOArray arHolder;
    [SerializeField] private ItemDBSO itemDB;
    [SerializeField] private MainTestSlot pfMainTestSlot;
    [SerializeField] private Transform testBtnSlot;
    [SerializeField] private Transform testBtnSlot2;

    [SerializeField] MainTestJoyStick joystick;
    [SerializeField] StickCancel cancelButton;
    [SerializeField] GameObject actSellect;
    [SerializeField] MainTestSlot sellectPlayer;
    [SerializeField] MainTestSlot[] armedItems;

    private GameObject attackAct;
    private Transform testBtnSlotParent;
    private Player testPlayer;
    private Enemy dummy;

    private void Start()
    {
        testBtnSlotParent = testBtnSlot.parent.parent.parent;
        MakeArTestSlot();
        joystick.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
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
        }
        foreach (ItemSO so in itemDB.items)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot2);
            instance.SetSO(so);
        }
    }

    public void ToggleSellectArPanel()
    {
        testBtnSlotParent.gameObject.SetActive(!testBtnSlotParent.gameObject.activeSelf);
    }

    public void SellectPlayer(Player player = null)
    {
        if(testPlayer!=null)
        {
            for (int i = 0; i < 3; i++) armedItems[i].UnSetItem();
            testPlayer.so.E_Item.itmeSO = new ItemSO[3];
            testPlayer.gameObject.SetActive(false);
        }
        testPlayer = player;
        if(player!=null)
        {
            sellectPlayer.GetAr(player.so);
            testPlayer.UnArmed();
        }
    }

    public void ArmedItem(ItemSO item)
    {
        if (testPlayer == null) return;
        foreach(MainTestSlot slot in armedItems)
        {
            if (slot.itemSO == null)
            {
                slot.GetItem(item);
                break;
            }
        }
    }

    public void SummonPlayer()
    {
        testPlayer.gameObject.SetActive(true);
        testPlayer.transform.position = Vector3.zero;
        testBtnSlotParent.gameObject.SetActive(false);
        testPlayer.UnArmed();
        for (int i = 0; i < 3; i++) testPlayer.so.E_Item.itmeSO[i] = armedItems[i].itemSO;
        testPlayer.GetItemEvents();
        testPlayer.StatReset();
        joystick.joystickType = JoystickType.None;
        joystick.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        actSellect.SetActive(true);
        attackAct.SetActive(testPlayer.isRangeCharacter);
        cameraMove.MovetoTarget(testPlayer.transform);
        dummy.StatReset();
        dummy.gameObject.SetActive(true);
        dummy.transform.position = new Vector3(5, 0);
        TurnManager.Instance.SomeoneIsMoving = false;
    }

    public void DragBegin()
    {
        cancelButton.gameObject.SetActive(true);
        cameraMove.MovetoTarget(testPlayer.transform);
        testPlayer?.DragBegin(joystick.joystickType);
    }

    public void Drag(float angle, float dis)
    {
        testPlayer?.Drag(angle, dis);
    }

    public void DragEnd(float power, Vector2 angle)
    {
        testPlayer.DisableRanges();
        if (cancelButton.entering)
        {
            cancelButton.gameObject.SetActive(false);
            cancelButton.entering = false;
            return;
        }

        testPlayer.DragEnd(joystick.joystickType, power, angle);
        cancelButton.gameObject.SetActive(false);
        testPlayer.CountCooltime();
    }

    public void ChooseStickType(int n)
    {
        if (testPlayer == null) return;
        if ((JoystickType)n == JoystickType.Skill && testPlayer.currentCooltime > 0) return;

        cameraMove.MovetoTarget(testPlayer.transform);
        joystick.gameObject.SetActive(true);
        joystick.joystickType = (JoystickType)n;
    }
}

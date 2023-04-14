using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTestModeManager : MonoSingleton<MainTestModeManager>
{
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private Enemy dummy;
    [SerializeField] private ArSOArray arHolder;
    [SerializeField] private MainTestSlot pfMainTestSlot;
    [SerializeField] private Transform testBtnSlot;

    [SerializeField] MainTestJoyStick joystick;
    [SerializeField] StickCancel cancelButton;
    [SerializeField] GameObject actSellect;

    private GameObject attackAct;
    private Transform testBtnSlotParent;
    private Player testPlayer;

    /* 
     * 여기에 해야할 것들
     * 2. 알을 골랐을 때 화면에 고른 알이 나오는 기능
     * 3. 알과 더미 위치가 초기화되는 기능
     * 4. 카메라를 알 위치로 이동(카메라무브 타겟을 알로)
     */

    //     * 1. 클릭하면 사용 가능한 알들을 표시하는 기능

    private void Start()
    {
        testBtnSlotParent = testBtnSlot.parent.parent;
        MakeArTestSlot();
        joystick.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        attackAct = actSellect.transform.GetChild(1).gameObject;
        actSellect.SetActive(false);
    }

    private void MakeArTestSlot()
    {
        foreach(ArSO so in arHolder.list)
        {
            var instance = Instantiate(pfMainTestSlot, testBtnSlot);
            instance.SetSO(so);
        }
    }

    public void ToggleSellectArPanel()
    {
        testBtnSlotParent.gameObject.SetActive(!testBtnSlotParent.gameObject.activeSelf);
    }

    public void SummonPlayer(Player newPlayer)
    {
        if(testPlayer != newPlayer)
        {
            testPlayer?.gameObject.SetActive(false);
            testPlayer = newPlayer;
            testPlayer.transform.position = Vector3.zero;
            testBtnSlotParent.gameObject.SetActive(false);
            joystick.joystickType = JoystickType.None;
            joystick.gameObject.SetActive(false);
            cancelButton.gameObject.SetActive(false);
            actSellect.SetActive(true);
            attackAct.SetActive(testPlayer.isRangeCharacter);
            cameraMove.MovetoTarget(testPlayer.transform);
        }
    }

    public void DragBegin()
    {
        cancelButton.gameObject.SetActive(true);
        testPlayer?.DragBegin(joystick.joystickType);
    }

    public void Drag(float angle)
    {
        testPlayer?.Drag(angle);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] InventoryObj deck;
    [SerializeField] QuickSlot pf_QuickSlot;
    [SerializeField] Transform slotHolder;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] JoyStick joystick;
    [SerializeField] StickCancel cancelButton;
    [SerializeField] GameObject actSellect;
    public Player sellectPlayer = null;
    private List<QuickSlot> quickSlots = new List<QuickSlot>();

    void Start()
    {
        SummonPlayers();
    }

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
        player.ColorChange(true);
    }

    public void DisableQuickSlots()
    {
        actSellect.SetActive(false);
        joystick.gameObject.SetActive(false);
        foreach (QuickSlot slot in quickSlots)
        {
            slot.ColorChange(false);
        }
    }

    public void DragBegin()
    {
        cancelButton.gameObject.SetActive(true);
    }
    public void Drag()
    {
        sellectPlayer?.Drag();
    }

    public void DragEnd(float power, Vector2 angle)
    {
        if(cancelButton.entering)
        {
            cancelButton.entering = false;
            cancelButton.gameObject.SetActive(false);
            return;
        }
        else if (TurnManager.Instance.UseTurn())
        {
            sellectPlayer.DragEnd(joystick.joystickType, power, angle);
            cancelButton.gameObject.SetActive(false);
            ResetSellect();
        }
    }

    public void ResetSellect()
    {
        sellectPlayer = null;
        DisableQuickSlots();
    }

    public void ChooseStickType(int n)
    {
        actSellect.SetActive(false);
        joystick.gameObject.SetActive(true);
        joystick.joystickType = (JoystickType)n;
    }
}

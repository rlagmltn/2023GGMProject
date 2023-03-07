using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] InventoryObj deck;
    [SerializeField] QuickSlot pf_QuickSlot;
    [SerializeField] Transform slotHolder;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] JoyStick moveStick;
    [SerializeField] JoyStick skillStick;
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
        moveStick.gameObject.SetActive(true);
        skillStick.gameObject.SetActive(true);
        DisableQuickSlots();
        player.ColorChange(true);
    }

    public void DisableQuickSlots()
    {
        foreach (QuickSlot slot in quickSlots)
        {
            slot.ColorChange(false);
        }
    }

    public void Drag()
    {
        if (sellectPlayer == null) return;
        sellectPlayer.Drag();
    }

    public void DragEnd(float power, Vector2 angle)
    {
        if (sellectPlayer != null && TurnManager.Instance.UseTurn())
        {
            sellectPlayer.DragEnd(power, angle);
            ResetSellect();
        }
    }

    public void ResetSellect()
    {
        sellectPlayer = null;
        moveStick.gameObject.SetActive(false);
        skillStick.gameObject.SetActive(false);
    }
}

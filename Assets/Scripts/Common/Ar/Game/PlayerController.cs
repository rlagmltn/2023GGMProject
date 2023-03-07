using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] InventoryObj deck;
    [SerializeField] QuickSlot pf_QuickSlot;
    [SerializeField] Transform slotHolder;
    public Player sellectPlayer = null;
    private List<QuickSlot> quickSlots = new List<QuickSlot>();

    void Start()
    {
        SummonPlayers();
    }

    private void SummonPlayers()
    {
        foreach(InventorySlot slot in deck.inventorySlots)
        {
            var player = Instantiate(slot.item, null);
            var quickSlot = Instantiate(pf_QuickSlot, slotHolder);
            quickSlots.Add(quickSlot);

            quickSlot.Connect(player);
        }
    }

    public void SellectPlayer(QuickSlot player)
    {
        sellectPlayer = player.Player;
        CameraMove.Instance.MovetoTarget(sellectPlayer.transform.position);
    }

    public void Drag()
    {
        if (sellectPlayer == null) return;
        sellectPlayer.Drag();
    }

    public void DragEnd(float power, Vector2 angle)
    {
        if (sellectPlayer == null) return;
        sellectPlayer.DragEnd(power, angle);
    }
}

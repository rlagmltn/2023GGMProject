using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlot : MonoBehaviour
{
    private Button button;
    public Player Player { get; set; }

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SellectPlayer);
    }

    public void Connect(Player player)
    {
        Player = player;
    }

    public void SellectPlayer()
    {
        PlayerController.Instance.SellectPlayer(this);
    }
}

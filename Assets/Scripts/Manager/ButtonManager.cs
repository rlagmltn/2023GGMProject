using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject background;
    private int[] doMovePos = { 1, 0, -1 };
    bool isBackgroundMove = false;


    [SerializeField] Button InventoryButton;
    [SerializeField] Button MainButton;
    [SerializeField] Button ShopButton;


    public enum STATE
    {
        INVENTORY,
        MAIN,
        SHOP
    }
    public STATE state = STATE.MAIN;

    private void Awake()
    {
        ButtonEventPlus(InventoryButton, InventoryButtonClick);
        ButtonEventPlus(ShopButton, ShopButtonClick);
        ButtonEventPlus(MainButton, MainButtonClick);
    }
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void InventoryButtonClick()
    {
        if (state == STATE.INVENTORY || isBackgroundMove) return;
        state = STATE.INVENTORY;
        MoveBackGround();
    }
    public void MainButtonClick()
    {
        if (state == STATE.MAIN || isBackgroundMove) return;
        state = STATE.MAIN;
        MoveBackGround();
    }
    public void ShopButtonClick()
    {
        if (state == STATE.SHOP || isBackgroundMove) return;
        state = STATE.SHOP;
        MoveBackGround();
    }
    private void MoveBackGround()
    {
        isBackgroundMove = true;
        background.transform.DOMoveX(transform.localPosition.x + -((int)state - 1) * 5.62f, 0.2f);
        isBackgroundMove = false;
    }

    void ButtonEventPlus(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}

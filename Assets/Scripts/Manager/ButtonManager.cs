using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public enum STATE
    {
        INVENTORY,
        MAIN,
        SHOP
    }
    public STATE state = STATE.MAIN;
    public void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void InventoryButton()
    {

    }
}

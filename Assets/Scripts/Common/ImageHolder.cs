using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : MonoBehaviour
{
    public ArSO Ar;
    public int num;
    public Image image;

    public void ClickArButton()
    {
        InventoryUI.Instance.Ar = Ar;
        InventoryUI.Instance.UpdateUI();
    }

    public void ClickButtonInGame()
    {
        InventorySelecter.Instance.unInputAr(num);
    }
}

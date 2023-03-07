using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHolder : MonoBehaviour
{
    public Image image;
    public int num;

    public void ClickArButton()
    {
        InventoryUI.Instance.Ar = InventorySorting.Instance.returnAr(num);
        InventoryUI.Instance.UpdateUI();
    }
}

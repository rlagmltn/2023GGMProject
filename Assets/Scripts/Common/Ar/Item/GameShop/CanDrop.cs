using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanDrop : MonoBehaviour
{
    [SerializeField] private Image ItemImage;
    [SerializeField] private int Num;

    private ArSO Ar;

    internal int GetNum()
    {
        return Num;
    }

    internal void SetCharacterSO(ArSO _AR)
    {
        Ar = _AR;
    }

    internal ArSO GetCharacterSO()
    {
        return Ar;
    }

    internal void UpdateUI()
    {
        ItemImage.sprite = Ar.E_Item.itmeSO[Num].itemIcon;
    }

    internal void ImageActiveSelf(bool IsImage)
    {
        ItemImage.gameObject.SetActive(IsImage);
    }

    internal void ItemUnEquip()
    {
        GameShop_Inventory.Instance.SetItem(Ar.E_Item.itmeSO[Num]);
        this.Ar.E_Item.itmeSO[Num] = null;
        GameShop_Character.Instance.AllArUpdateUI();
        Ar.ArData.StatReset();
    }

    internal void CharacterStatReset()
    {
        //Ar.ArData.StatReset(); //나중에 켜줘야함
    }
}

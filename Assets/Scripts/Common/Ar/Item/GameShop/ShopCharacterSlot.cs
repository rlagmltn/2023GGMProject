using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterSlot : MonoBehaviour
{
    [SerializeField] private Image ArImage;
    [SerializeField] private List<Button> ItemButtons;

    private ArSO AR;

    internal void UpdateUI()
    {
        ArImage.sprite = AR.characterInfo.Image;
        
        for(int num = 0; num < AR.E_Item.itmeSO.Length; num++)
            if (AR.E_Item.itmeSO[num] == null)
                ItemButtons[num].interactable = false;
    }

    internal void SetArSO(ArSO Ar)
    {
        AR = Ar;
        UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCharacterSlot : MonoBehaviour
{
    [SerializeField] private Image ArImage;
    [SerializeField] private List<Button> ItemButtons;

    public bool isMap = false;
    private ArSO AR;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int num = 0; num < AR.E_Item.itmeSO.Length; num++)
        {
            ItemButtons[num].onClick.RemoveAllListeners();
            if(isMap)
            {
                ItemButtons[num].onClick.AddListener(ItemButtons[num].GetComponent<CanDrop>().Map_ItemUnEquip);
                continue;
            }
            ItemButtons[num].onClick.AddListener(ItemButtons[num].GetComponent<CanDrop>().ItemUnEquip);
        }
        //ImageButton.onClick.RemoveAllListeners();
        //ImageButton.onClick.AddListener(() => ItemUIInfo.Instance.UpdateUI_AR(AR));
    }

    internal void UpdateUI()
    {
        ArImage.sprite = AR.characterInfo.Image;
        
        for(int num = 0; num < AR.E_Item.itmeSO.Length; num++)
        {
            if (AR.E_Item.itmeSO[num] == null)
            {
                ItemButtons[num].interactable = false;
                ItemButtons[num].GetComponent<CanDrop>().ImageActiveSelf(false);
            }
            else
            {
                ItemButtons[num].interactable = true;
                ItemButtons[num].GetComponent<CanDrop>().ImageActiveSelf(true);
                ItemButtons[num].GetComponent<CanDrop>().UpdateUI();
            }
        }
    }

    internal void SetArSO(ArSO Ar)
    {
        AR = Ar;
        InputInformation();
        UpdateUI();
    }

    private void InputInformation()
    {
        for(int num = 0; num < ItemButtons.Count; num++)
            ItemButtons[num].GetComponent<CanDrop>().SetCharacterSO(AR);
    }
}

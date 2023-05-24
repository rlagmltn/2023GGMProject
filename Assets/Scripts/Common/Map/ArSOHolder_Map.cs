using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArSOHolder_Map : MonoBehaviour
{
    private ArSO Ar;
    private Image ArImage;

    Color color = new Color(1, 1, 1);

    private void UpdateButtonUI()
    {
        ArImage = transform.GetChild(0).GetComponent<Image>();
        ArImage.sprite = Ar.characterInfo.Image;
    }

    private void Button_ActiveSelf()
    {
        if (Ar.isInGameTake == false)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

    public void SetArSO(ArSO ar)
    {
        if (ar == null)
        {
            gameObject.SetActive(false);
            return;
        }

        Ar = ar;
        UpdateButtonUI();
        Button_ActiveSelf();
    }

    public ArSO GetArSO()
    {
        return Ar;
    }

    public void SelectedButton()
    {
        MapInventory.Instance.SelectArSO(Ar);
    }

    public void SelectButton_Heal()
    {
        Heal_Inventory.Instance.gameObject.GetComponent<AllHealEvent>().HealFull(Ar);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArSOHolder_Map : MonoBehaviour
{
    private ArSO Ar;
    private Image ArImage;
    private Transform HPBar;

    Color color = new Color(1, 1, 1);

    private void UpdateButtonUI()
    {
        ArImage = transform.GetChild(0).GetComponent<Image>();
        HPBar = transform.GetChild(3);
        ArImage.sprite = Ar.characterInfo.Image;
        HPBar.localScale = new Vector3((float)(Ar.surviveStats.currentHP / Ar.surviveStats.MaxHP), 1, 1);
        HPBarColorChange();
    }

    private void HPBarColorChange()
    {
        float HPPercent = (float)Ar.surviveStats.currentHP / Ar.surviveStats.MaxHP;
        float HP = HPPercent * 100;

        color = HP switch
        {
            >= 75 => new Color(0.2f, 0.949f, 0.2f, 1),
            >= 50 and < 75 => new Color(0.949f, 0.6969f, 0.2f, 1),
            >= 25 and < 50 => new Color(0.9352f, 0.949f, 0.2f, 1),
            < 25 => new Color(0.95f, 0.2f, 0.2f, 1),
            _ => new Color(1, 1, 1, 1),
        };

        HPBar.GetComponent<Image>().color = color;
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

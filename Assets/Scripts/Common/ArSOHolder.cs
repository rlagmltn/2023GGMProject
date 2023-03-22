using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArSOHolder : MonoBehaviour
{
    private ArSO Ar;
    private Image ArImage;
    private Image SelectedImage;

    Color color = new Color(1, 1, 1);

    private void UpdateButtonUI()
    {
        ArImage = transform.GetChild(0).GetComponent<Image>();
        SelectedImage = transform.GetChild(1).GetComponent<Image>();
        ArImage.sprite = Ar.Image;
    }

    private void Button_ActiveSelf()
    {
        if (Ar == null)
        {
            gameObject.SetActive(false);
            return;
        }

        if(Ar.isTake == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            color = gameObject.GetComponent<Button>().colors.disabledColor;
            ArImage.color = color;
            return;
        }

        gameObject.GetComponent<Button>().interactable = true;
        ArImage.color = new Color(1f, 1f, 1f, 1f);
    }

    public void SetArSO(ArSO ar)
    {
        Ar = ar;
        UpdateButtonUI();
        Button_ActiveSelf();
    }

    public ArSO GetArSO()
    {
        return Ar;
    }

    public void SelectedButton() //컬러 체인지 작업해야함
    {
        if(Ar.isUse)
        {
            Ar.isUse = false;
            SelectedImage.gameObject.SetActive(false);
            ArInventorySelecter.Instance.UnselectArSO(Ar);
            return;
        }

        if(!ArInventorySelecter.Instance.IsCanSelect()) return;

        Ar.isUse = true;
        SelectedImage.gameObject.SetActive(true);
        ArInventorySelecter.Instance.SelectArSO(Ar);
    }
}

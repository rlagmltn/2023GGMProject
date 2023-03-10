using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

public class InventorySorting : MonoSingleton<InventorySorting>
{
    [SerializeField] Transform content;

    private ArSOList ArList;

    public List<Transform> Buttons;

    public List<Transform> HaveButton;
    public List<Transform> nHaveButton;

    private void Awake()
    {
        ArList = GetComponent<ArSOListHolder>().ArList;
        SetButtons();
        SortInventory();
    }

    void SetButtons()
    {
        int num = 0;
        foreach (Button button in content.GetComponentsInChildren<Button>()) //transform바꾸자
        {
            Buttons.Add(button.transform);

            ImageHolder img = button.GetComponent<ImageHolder>();
            //img.Ar = ArList.list[num]; //여기서 버그날거임

            AddButtonEvent(button, img.ClickArButton);
            num++;
        }
        //Buttons = content.GetComponentsInChildren<Button>();

        //for(int num = 0; num < Buttons.Length; num++)
        //{
        //    Buttons[num].onClick.AddListener(() => ClickArButton(num));
        //    Buttons[num].gameObject.SetActive(false);
        //}
    }

    void AllButtonActiveFalse()
    {
        foreach(Transform btn in Buttons)
        {
            btn.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 인벤토리를 정렬 해주는 함수
    /// </summary>
    public void SortInventory()
    {
        AllButtonActiveFalse();

        ImageHolder img;
        int num = 0;

        for (num = 0; num < ArList.list.Count; num++)
        {
            Buttons[num].gameObject.SetActive(true);
        }

        num = 0;
        foreach(ArSO ar in ArList.list)
        {
            if(ar.isTake && !ar.isUse)
            {
                ActiveClick(Buttons[num], true);

                img = Buttons[num].GetComponent<ImageHolder>();
                img.Ar = ar;

                img.image.sprite = img.Ar.Image;
                img.image.color = Color.white;

                num++;
            }
        }

        foreach(ArSO ar in ArList.list)
        {
            if (!ar.isTake || ar.isUse)
            {
                ActiveClick(Buttons[num], false);

                img = Buttons[num].GetComponent<ImageHolder>();
                img.Ar = ar;

                img.image.sprite = img.Ar.Image;
                img.image.color = Color.gray;

                num++;
            }
        }
    }

    void ActiveClick(Transform button, bool canClick)
    {
        button.gameObject.GetComponent<Button>().interactable = canClick;
    }
    
    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }

    public ArSO returnAr(int num)
    {
        return ArList.list[num];
    }
}

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
            img.num = num;

            AddButtonEvent(button, img.ClickArButton);
            button.transform.gameObject.SetActive(false);
            num++;
        }

        //Buttons = content.GetComponentsInChildren<Button>();

        //for(int num = 0; num < Buttons.Length; num++)
        //{
        //    Buttons[num].onClick.AddListener(() => ClickArButton(num));
        //    Buttons[num].gameObject.SetActive(false);
        //}
    }


    /// <summary>
    /// 인벤토리를 정렬 해주는 함수
    /// </summary>
    void SortInventory()
    {
        int takeCount = 0;
        int buttonCount = 0;
        bool canClick = false;
        Color color = Color.white;
        Image image;

        foreach (ArSO ar in ArList.list)
        {
            if (ar.isTake) takeCount++;

            buttonCount++;
        }

        // 버튼에 비활성화 하는 스크립트 따로 만들어서 붙이기

        for (int i = 0; i < buttonCount; i++)
        {
            Buttons[i].gameObject.SetActive(true);
            color = new Color(0.7843f, 0.7843f, 0.7843f, 0.5f);
            canClick = false;

            if (i < takeCount) 
            {
                canClick = true;
                color = Color.white;
            }

            image = Buttons[i].GetComponent<ImageHolder>().image;
            image.color = color;
            image.sprite = ArList.list[i].Image;

            ActiveClick(Buttons[i].transform, canClick);
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

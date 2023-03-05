using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySorting : MonoBehaviour
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
        foreach (Image image in content.GetComponentsInChildren<Image>()) //transform�ٲ���
        {
            Buttons.Add(image.transform);
            image.transform.gameObject.SetActive(false);
        }
    }

    void SortInventory()
    {
        int takeCount = 0;
        int buttonCount = 0;
        bool canClick = false;

        foreach (ArSO ar in ArList.list)
        {
            if (ar.isTake) takeCount++;

            buttonCount++;
        }

        // ��ư�� ��Ȱ��ȭ �ϴ� ��ũ��Ʈ ���� ���� ���̱�

        for (int i = 0; i < buttonCount; i++)
        {
            Buttons[i].gameObject.SetActive(true);
            //color = Color.gray;
            canClick = false;

            if (i < takeCount) canClick = true;

            ActiveClick(Buttons[i], canClick);
        }
    }

    void ActiveClick(Transform button, bool canClick)
    {
        button.gameObject.GetComponent<Button>().interactable = canClick;
    }
}

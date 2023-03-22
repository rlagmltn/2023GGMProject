using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArInventorySelecter : MonoSingleton<ArInventorySelecter>
{
    [SerializeField] private List<Button> buttons;
    [SerializeField] private ArSOList ArList;
    [SerializeField] private ArSO emptyAr;
    [SerializeField] private int CanSelectNum;

    private List<ArSO> sortedArSO;
    private ArSO[] selectedArs;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ClassfyArSO();
        ButtonInit();
        SelectedSOInit();
    }

    /// <summary>
    /// �ִ� �˰� ���� ���� �з��ϴ� �۾�
    /// </summary>
    void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if(ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);

        for (int num = 0; num < ArList.list.Count; num++)
            if (!ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);
    }

    /// <summary>
    /// ��ư���� ó�� �ʱ�ȭ ���ִ� �Լ�
    /// </summary>
    void ButtonInit()
    {
        for (int num = 0; num < buttons.Count; num++)
        {
            buttons[num].GetComponent<ArSOHolder>().SetArSO(sortedArSO[num]);
            RemoveAllButtonEvents(buttons[num]);
            AddButtonEvent(buttons[num], buttons[num].GetComponent<ArSOHolder>().SelectedButton);
        }
    }

    /// <summary>
    /// ���õ� SO���� �ʱ�ȭ���ִ� �Լ�
    /// </summary>
    void SelectedSOInit()
    {
        selectedArs = new ArSO[CanSelectNum];

        for (int num = 0; num < selectedArs.Length; num++)
            selectedArs[num] = emptyAr;
    }

    void RemoveAllButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ�� �߰����ִ� �Լ�
    /// </summary>
    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// ���� �� �� �ִ� �������� �������ִ� �Լ�
    /// </summary>
    /// <returns></returns>
    public bool IsCanSelect()
    {
        for(int num = 0; num < selectedArs.Length; num++)
            if(selectedArs[num] == emptyAr) return true;
        return false;
    }

    /// <summary>
    /// EmptySO�� �Ϳ� Select�����ִ� �Լ�
    /// </summary>
    /// <param name="ar"></param>
    public void SelectArSO(ArSO ar)
    {
        for (int num = 0; num < selectedArs.Length; num++)
        {
            if (selectedArs[num] == emptyAr)
            {
                selectedArs[num] = ar;
                break;
            }
        }
    }
    
    /// <summary>
    /// Select�� SO�� EmptySO�� �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="ar"></param>
    public void UnselectArSO(ArSO ar)
    {
        for (int num = 0; num < selectedArs.Length; num++)
        {
            if (selectedArs[num] == ar)
            {
                selectedArs[num] = emptyAr;
                break;
            }
        }
    }
}

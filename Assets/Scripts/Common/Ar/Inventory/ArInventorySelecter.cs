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
    /// 있는 알과 없는 알을 분류하는 작업
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
    /// 버튼들을 처음 초기화 해주는 함수
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
    /// 선택된 SO들을 초기화해주는 함수
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
    /// 버튼에 이벤트를 추가해주는 함수
    /// </summary>
    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    /// <summary>
    /// 선택 할 수 있는 상태인지 리턴해주는 함수
    /// </summary>
    /// <returns></returns>
    public bool IsCanSelect()
    {
        for(int num = 0; num < selectedArs.Length; num++)
            if(selectedArs[num] == emptyAr) return true;
        return false;
    }

    /// <summary>
    /// EmptySO인 것에 Select시켜주는 함수
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
    /// Select된 SO를 EmptySO로 바꿔주는 함수
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

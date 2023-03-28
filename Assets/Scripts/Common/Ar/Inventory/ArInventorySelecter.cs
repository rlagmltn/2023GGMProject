using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ArInventorySelecter : MonoSingleton<ArInventorySelecter>
{
    [SerializeField] private List<Button> PresetButtons;
    [SerializeField] private ArSOList ArList;
    [SerializeField] private ArSO emptyAr;
    [SerializeField] private List<Button> Buttons;
    [SerializeField] private List<Image> ArImages;

    private List<ArSO> sortedArSO;
    public ArSO[] FirstPreset;
    public ArSO[] SecondPreset;
    public ArSO[] ThirdPreset;

    private int SelectPresetNum;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        FirstPreset = new ArSO[3];
        SecondPreset = new ArSO[3];
        ThirdPreset = new ArSO[3];
        SelectPresetNum = 0;

        SetEmptySO();
        ClassfyArSO();
        ButtonInit();
        UpdateUI();
    }
    
    /// <summary>
    /// 알들을 정렬 하는 함수
    /// </summary>
    private void ClassfyArSO()
    {
        sortedArSO = new List<ArSO>();

        for (int num = 0; num < ArList.list.Count; num++)
            if (ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);

        for (int num = 0; num < ArList.list.Count; num++)
            if (!ArList.list[num].isTake) sortedArSO.Add(ArList.list[num]);

        AddButtonArSO();
    }

    void AddButtonArSO()
    {
        int num = 0;
        foreach (Button btn in Buttons)
        {
            if(sortedArSO.Count <= num)
            {
                btn.GetComponent<ArSOHolder>().SetArSO(null);
                num++;
                continue;
            }
            btn.GetComponent<ArSOHolder>().SetArSO(sortedArSO[num]);
            num++;
        }
    }

    void SetEmptySO()
    {
        for(int i = 0; i < FirstPreset.Length; i++)
        {
            FirstPreset[i] = emptyAr;
            SecondPreset[i] = emptyAr;
            ThirdPreset[i] = emptyAr;
        }
    }

    void UpdateUI()
    {
        ArSO[] arList = SelectPresetNum switch
        {
            0 => FirstPreset,
            1 => SecondPreset,
            2 => ThirdPreset,
            _ => FirstPreset,
        };

        int num = 0;

        foreach(ArSO ar in arList)
        {
            ArImages[num].sprite = ar.Image;
            ++num;
        }
    }

    /// <summary>
    /// ArSO에 알을 선택 할 수 잇는지 리턴해주는 함수
    /// </summary>
    /// <returns></returns>
    public bool CanSelect()
    {
        ArSO[] arList = ReturnPresetList();

        foreach (ArSO ar in arList)
        {
            if(ar == emptyAr)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// ArSO에 알을 선택해주는 함수
    /// </summary>
    /// <param name="ar"></param>
    public void SelectArSO(ArSO ar)
    {
        ArSO[] arList = ReturnPresetList();

        for (int i = 0; i < arList.Length; i++)
        {
            if(arList[i] == emptyAr)
            {
                arList[i] = ar;
                break;
            }
        }

        UpdateUI();
    }

    ArSO[] ReturnPresetList()
    {
        ArSO[] arList = SelectPresetNum switch
        {
            0 => FirstPreset,
            1 => SecondPreset,
            2 => ThirdPreset,
            _ => FirstPreset,
        };

        return arList;
    }

    void ButtonInit()
    {
        RemoveAllButtonEvents(PresetButtons[0]);
        RemoveAllButtonEvents(PresetButtons[1]);
        RemoveAllButtonEvents(PresetButtons[2]);
        AddButtonEvent(PresetButtons[0], FirstPresetButtonClick);
        AddButtonEvent(PresetButtons[1], SecondPresetButtonClick);
        AddButtonEvent(PresetButtons[2], ThirdPresetButtonClick);

        foreach(Button btn in Buttons)
        {
            RemoveAllButtonEvents(btn);
            AddButtonEvent(btn, btn.GetComponent<ArSOHolder>().SelectedButton);
        }

        RemoveAllButtonEvents(ArImages[0].GetComponent<Button>());
        RemoveAllButtonEvents(ArImages[1].GetComponent<Button>());
        RemoveAllButtonEvents(ArImages[2].GetComponent<Button>());

        AddButtonEvent(ArImages[0].GetComponent<Button>(), FirstImageClick);
        AddButtonEvent(ArImages[1].GetComponent<Button>(), SecondImageClick);
        AddButtonEvent(ArImages[2].GetComponent<Button>(), ThirdImageClick);
    }

    void FirstPresetButtonClick()
    {
        SelectPresetNum = 0;
        ChangePreset();
        AllUIUpdate();
    }

    void SecondPresetButtonClick()
    {
        SelectPresetNum = 1;
        ChangePreset();
        AllUIUpdate();
    }
    void ThirdPresetButtonClick()
    {
        SelectPresetNum = 2;
        ChangePreset();
        AllUIUpdate();
    }

    void ChangePreset()
    {
        for (int i = 0; i < FirstPreset.Length; i++)
        {
            FirstPreset[i].isUse = false;
            SecondPreset[i].isUse = false;
            ThirdPreset[i].isUse = false;
        }

        ArSO[] arList = ReturnPresetList();

        for (int i = 0; i < arList.Length; i++)
        {
            arList[i].isUse = true;
        }
    }

    void FirstImageClick()
    {
        ArSO[] arList = ReturnPresetList();

        arList[0].isUse = false;
        arList[0] = emptyAr;
        AllUIUpdate();
    }

    void SecondImageClick()
    {
        ArSO[] arList = ReturnPresetList();

        arList[1].isUse = false;
        arList[1] = emptyAr;
        AllUIUpdate();
    }

    void ThirdImageClick()
    {
        ArSO[] arList = ReturnPresetList();

        arList[2].isUse = false;
        arList[2] = emptyAr;
        AllUIUpdate();
    }

    void AllUIUpdate()
    {
        foreach (Button btn in Buttons)
        {
            ArSOHolder holder =  btn.GetComponent<ArSOHolder>();
            holder.SetArSO(holder.GetArSO());
        }
        UpdateUI();
    }

    public void ArTOGlobal()
    {
        for(int i = 0; i < FirstPreset.Length; i++)
        {
            Global.FirstPreset[i] = FirstPreset[i];
            Global.SecondPreset[i] = SecondPreset[i];
            Global.ThirdPreset[i] = ThirdPreset[i];
        }
    }

    void RemoveAllButtonEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

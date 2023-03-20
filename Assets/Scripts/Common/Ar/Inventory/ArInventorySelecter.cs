using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class ArInventorySelecter : MonoSingleton<ArInventorySelecter>
{
    [SerializeField] private Button selectButton;
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<TextMeshProUGUI> texts;
    [SerializeField] private List<Image> images;
    public ArSO emptyAr;
    [SerializeField] private Button startButton;
    [SerializeField] private Transform warningPanel;
    [SerializeField] private Button quitButton;

    public ArSO[] Ars;

    private void Start()
    {
        Ars = new ArSO[3];
        Init();
        AddButtonEvent(selectButton, InputAr);
        AddButtonEvent(startButton, StartButtonClick);
        AddButtonEvent(quitButton, QuitButtonClick);
    }

    /// <summary>
    /// 시작할때 초기화
    /// </summary>
    void Init()
    {
        for (int i = 0; i < Ars.Length; i++)
        {
            Ars[i] = emptyAr;
        }
        UpdateUI();

        ImageHolder img;

        for (int i = 0; i < buttons.Count; i++)
        {
            img = buttons[i].gameObject.GetComponent<ImageHolder>();
            img.num = i;
            AddButtonEvent(buttons[i], img.ClickButtonInGame);
        }
    }

    /// <summary>
    /// select 버튼 눌렀을때 입력하기
    /// </summary>
    public void InputAr()
    {
        for(int i = 0; i < Ars.Length; i++)
        {
            if(Ars[i] == emptyAr)
            {
                Ars[i] = InventoryUI.Instance.Ar;
                InventoryUI.Instance.Ar.isUse = true;
                InventoryUI.Instance.Ar = emptyAr;
                InventoryUI.Instance.UpdateUI();
                ArInventorySorting.Instance.SortInventory();
                UpdateUI();
                return;
            }
        }
    }

    /// <summary>
    /// UI의 상태를 업데이트 해줌
    /// </summary>
    public void UpdateUI()
    {
        for (int i = 0; i < Ars.Length; i++)
        {
            images[i].sprite = Ars[i].Image;
            texts[i].text = Ars[i].Name;
        }
    }

    public void unInputAr(int num)
    {
        images[num].sprite = null;
        texts[num].text = "";
        Ars[num].isUse = false;
        Ars[num] = emptyAr;
        UpdateUI();
        ArInventorySorting.Instance.SortInventory();
    }

    void StartButtonClick()
    {
        if(CanStart())
            MGScene.Instance.ChangeScene(eSceneName.Son);
        else
            warningPanel.gameObject.SetActive(true);
    }

    bool CanStart()
    {
        foreach(ArSO ar in Ars)
        {
            if (ar == emptyAr)
            {
                return false;
            }
        }
        return true;
    }

    void QuitButtonClick()
    {
        warningPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 버튼에 이벤트를 추가해주는 함수
    /// </summary>
    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
}

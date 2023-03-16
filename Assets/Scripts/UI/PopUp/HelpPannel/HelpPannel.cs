using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// 도움말 패널 관련 코드
/// </summary>
public class HelpPannel : MonoSingleton<HelpPannel>
{
    [SerializeField] private List<Transform> pannelImages;
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private List<Button> underButtons;
    [SerializeField] private Button helpButton;

    public int pannelNum = 0;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 초기화 시키는 함수
    /// </summary>
    private void Init()
    {
        for(int num = 0; num < pannelImages.Count; num++)
        {
            pannelImages[num].gameObject.SetActive(false);
            underButtons[num].GetComponent<IntHolder>().SetNum(num);
            RemoveAllEvents(underButtons[num]);
            AddButtonEvents(underButtons[num], underButtons[num].GetComponent<IntHolder>().GetNum_Help);
        }

        RemoveAllEvents(leftButton);
        RemoveAllEvents(rightButton);
        AddButtonEvents(leftButton, pannelNumMinus);
        AddButtonEvents(rightButton, pannelNumPlus);
        UpdateUI();
    }

    /// <summary>
    /// 선택된 패널을 정해진 인덱스로 바꿔주는 함수
    /// </summary>
    /// <param name="num"></param>
    public void SetPannelNum(int num)
    {
        pannelNum = num;
        UpdateUI();
    }

    /// <summary>
    /// UI를 업데이트 시키는 함수
    /// </summary>
    public void UpdateUI()
    {
        for (int num = 0; num < pannelImages.Count; num++)
        {
            pannelImages[num].gameObject.SetActive(false);
            underButtons[num].GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f);

            if (num == pannelNum)
            {
                pannelImages[num].gameObject.SetActive(true);
                underButtons[num].GetComponent<Image>().color = Color.white;
            }
        }

        //underButton 색칠하기도 해야할듯?
    }

    /// <summary>
    /// 선택된 패널을 0번째 인덱스로 초기화 시키는 함수
    /// </summary>
    public void ResetPannelNum()
    {
        pannelNum = 0;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
    }

    /// <summary>
    /// 다음 패널로 넘어갈 수 있게 해주는 함수
    /// </summary>
    private void pannelNumPlus()
    {
        pannelNum++;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
        UpdateUI();
    }

    /// <summary>
    /// 이전 패널로 돌아가게 해주는 함수
    /// </summary>
    private void pannelNumMinus()
    {
        pannelNum--;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
        UpdateUI();
    }

    /// <summary>
    /// 버튼의 모든 이벤트를 지워주는 함수
    /// </summary>
    /// <param name="button"></param>
    private void RemoveAllEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 버튼에 이벤트를 더해주는 함수
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    private void AddButtonEvents(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

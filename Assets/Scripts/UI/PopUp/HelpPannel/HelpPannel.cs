using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// ���� �г� ���� �ڵ�
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
    /// �ʱ�ȭ ��Ű�� �Լ�
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
    /// ���õ� �г��� ������ �ε����� �ٲ��ִ� �Լ�
    /// </summary>
    /// <param name="num"></param>
    public void SetPannelNum(int num)
    {
        pannelNum = num;
        UpdateUI();
    }

    /// <summary>
    /// UI�� ������Ʈ ��Ű�� �Լ�
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

        //underButton ��ĥ�ϱ⵵ �ؾ��ҵ�?
    }

    /// <summary>
    /// ���õ� �г��� 0��° �ε����� �ʱ�ȭ ��Ű�� �Լ�
    /// </summary>
    public void ResetPannelNum()
    {
        pannelNum = 0;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
    }

    /// <summary>
    /// ���� �гη� �Ѿ �� �ְ� ���ִ� �Լ�
    /// </summary>
    private void pannelNumPlus()
    {
        pannelNum++;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
        UpdateUI();
    }

    /// <summary>
    /// ���� �гη� ���ư��� ���ִ� �Լ�
    /// </summary>
    private void pannelNumMinus()
    {
        pannelNum--;
        pannelNum = Mathf.Clamp(pannelNum, 0, 4);
        UpdateUI();
    }

    /// <summary>
    /// ��ư�� ��� �̺�Ʈ�� �����ִ� �Լ�
    /// </summary>
    /// <param name="button"></param>
    private void RemoveAllEvents(Button button)
    {
        button.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// ��ư�� �̺�Ʈ�� �����ִ� �Լ�
    /// </summary>
    /// <param name="button"></param>
    /// <param name="action"></param>
    private void AddButtonEvents(Button button, UnityAction action)
    {
        button.onClick.AddListener(action);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
public class HitOrRun : _Event
{
    [SerializeField] private Transform HORPannel;
    [SerializeField] private ArSOList ArList;

    [SerializeField] private List<Button> SelectedOptionButton;

    [SerializeField] private Transform BackGroundPannel;
    [SerializeField] private Transform Option1Pannel;
    [SerializeField] private Transform Option2Pannel;
    [SerializeField] private Transform Option3Pannel;

    [SerializeField] private TextMeshProUGUI Option1Pannel_Text;
    [SerializeField] private TextMeshProUGUI Option2Pannel_Text;
    [SerializeField] private TextMeshProUGUI Option3Pannel_Text;

    [SerializeField] private Transform Option1Pannel_Image;
    [SerializeField] private Image Option1Pannel_Image_HP;

    [SerializeField] private List<Button> StageButton;

    private List<ArSO> Ars;

    public override void EventStart()
    {
        GetIsTakeArs();
        EventInit();
    }

    void EventInit()
    {
        HORPannel.gameObject.SetActive(true);
        ButtonInit();
        TwinManager_Event.Instance.SetDAP(0.7f, SelectedOptionButton[0].transform);
        TwinManager_Event.Instance.SetDAP(0.2f, SelectedOptionButton[1].transform);
        TwinManager_Event.Instance.SetDAP(0.2f, SelectedOptionButton[2].transform);
        TwinManager_Event.Instance.EventStart();
    }

    void ButtonInit()
    {
        SelectedOptionButton[0].onClick.RemoveAllListeners();
        SelectedOptionButton[1].onClick.RemoveAllListeners();
        SelectedOptionButton[2].onClick.RemoveAllListeners();

        SelectedOptionButton[0].onClick.AddListener(FirstOption);
        SelectedOptionButton[1].onClick.AddListener(SecondOption);
        SelectedOptionButton[2].onClick.AddListener(ThirdOption);

        foreach(Button btn in StageButton)
        {
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(EventManger.Instance.StageClear);
        }
    }

    void FirstOption()
    {
        int Rnum = Random.Range(0, Ars.Count);
        Ars[Rnum].surviveStats.currentHP -= 2f;
        //�̰� �ϰ� �г� ��������
        //Option1Pannel_Text.text = $"{Ars[Rnum].characterInfo.Name}�� ü���� 2�Ҿ����ϴ�. ����ü���� {Ars[Rnum].surviveStats.currentHP} �Դϴ�.";
        Option1Pannel_Text.text = $"ü�� 2���� \n����ü�� : {Ars[Rnum].surviveStats.currentHP}";
        Option1Pannel_Image_HP.fillAmount = (float)(Ars[Rnum].surviveStats.currentHP / Ars[Rnum].surviveStats.MaxHP);
        var dmy = Instantiate(Ars[Rnum].DMY, Option1Pannel_Image);
        dmy.transform.localScale = new Vector3(100, 100, 0);
        dmy.SetParent(Option1Pannel_Image);
        dmy.localPosition = new Vector2(125, -125);
        BackGroundPannel.gameObject.SetActive(true);
        Option1Pannel.gameObject.SetActive(true);
        dmy.transform.GetComponent<AnimationManager>().Hit();
    }

    void SecondOption()
    {
        //���� ����
        //BackGroundPannel.gameObject.SetActive(true);
        //Option2Pannel.gameObject.SetActive(true);
        Global.isEventBattle = true;
        Global.isEvnetSave = true;

        SceneManager.LoadScene("TestScene");
        
    }

    void ThirdOption()
    {
        Option3Pannel_Text.text = "������ �����ƴ�.";
        //�ʾ����� ��������
        BackGroundPannel.gameObject.SetActive(true);
        Option3Pannel.gameObject.SetActive(true);
    }

    void GetIsTakeArs()
    {
        Ars = new List<ArSO>();
        foreach (ArSO ar in ArList.list)
        {
            if (!ar.isInGameTake) continue;
            Ars.Add(ar);
        }
    }
}

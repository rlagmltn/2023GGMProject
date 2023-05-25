using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManger : MonoSingleton<EventManger>
{
    [SerializeField] private Button ClearButton;
    [SerializeField] private Transform BackGroudnPannel;
    [SerializeField] private Transform ClearPannel;

    [SerializeField] private List<_Event> EventList;
    public Events S_event;

    private int RandomNum;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        ButtonInit();
        EventPlay();
    }

    void ButtonInit()
    {
        ClearButton.onClick.RemoveAllListeners();
        ClearButton.onClick.AddListener(StageClear);
    }

    void EventPlay()
    {
        EventSelect();
        
        switch(S_event)
        {
            case Events.HOR: { EventList[(int)Events.HOR].EventStart(); } break;
            case Events.HealAr: { EventList[(int)Events.HealAr].EventStart();} break;
            default: { EventList[(int)S_event].EventStart();} break;
        }
    }

    void EventSelect()
    {
        if(Global.isEvnetSave)
        {
            S_event = Global.G_Event;
            Global.isEvnetSave = false;
            EventClear();
            return;
        }

        RandomNum = Random.Range(0, EventList.Count);

        foreach(Events num in Events.GetValues(typeof(Events)))
        {
            if(RandomNum == (int)num)
            {
                string EventName = Events.GetName(typeof(Events), RandomNum);
                S_event = (Events)Events.Parse(typeof(Events), EventName);
                break;
            }
        }
    }

    internal void EventClear() //이벤트가 클리어왰을때 띄워줄 창
    {
        BackGroudnPannel.gameObject.SetActive(true);
        ClearPannel.gameObject.SetActive(true);
    }

    public void StageClear()
    {
        Global.EnterStage.IsCleared = true;
        SceneManager.LoadScene("Stage1Map");
    }
}

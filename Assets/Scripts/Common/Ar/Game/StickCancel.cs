using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StickCancel : MonoBehaviour
{
    EventTrigger eventTrigger;
    public bool entering;

    private void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry enterTrigger = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
        EventTrigger.Entry exitTrigger = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
        enterTrigger.callback.AddListener(OnEnter);
        exitTrigger.callback.AddListener(OnExit);
        eventTrigger.triggers.Add(enterTrigger);
        eventTrigger.triggers.Add(exitTrigger);
    }

    private void OnEnter(BaseEventData data)
    {
        entering = true;
    }

    private void OnExit(BaseEventData data)
    {
        entering = false;
    }
}

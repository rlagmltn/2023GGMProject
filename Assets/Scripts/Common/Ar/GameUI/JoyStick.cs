using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour
{
    [SerializeField] float radius;
    [SerializeField] bool isMoveStick;
    private Transform stick;
    private Vector3 stickVec;
    private Vector3 defaultPos;
    EventTrigger eventTrigger;

    private void Start()
    {
        stick = transform.GetChild(0);
        eventTrigger = stick.GetComponent<EventTrigger>();

        defaultPos = stick.position;

        EventTrigger.Entry dragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        EventTrigger.Entry endDragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
        dragTrigger.callback.AddListener(OnDrag);
        endDragTrigger.callback.AddListener(OnDragEnd);
        eventTrigger.triggers.Add(dragTrigger);
        eventTrigger.triggers.Add(endDragTrigger);
    }

    public void OnDrag(BaseEventData data)
    {
        Vector3 Pos = Util.Instance.mousePosition;

        stickVec = (Pos - defaultPos).normalized;

        float Dis = Vector3.Distance(Pos, defaultPos);

        if (Dis < radius)
            stick.position = defaultPos + stickVec * Dis;
        else
            stick.position = defaultPos + stickVec * radius;
    }

    public void OnDragEnd(BaseEventData data)
    {
        stick.position = defaultPos;
        //if(isMoveStick)

    }
        
}

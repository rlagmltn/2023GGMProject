using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum JoystickType : int
{
    Move,
    Attack,
    Skill,
    None
}

public class JoyStick : MonoBehaviour
{
    [SerializeField] float radius;
    public JoystickType joystickType;
    private Transform stick;
    private Vector3 stickVec;
    EventTrigger eventTrigger;

    private void Start()
    {
        stick = transform.GetChild(0);
        eventTrigger = stick.GetComponent<EventTrigger>();

        EventTrigger.Entry beginDragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.BeginDrag };
        EventTrigger.Entry dragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        EventTrigger.Entry endDragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
        beginDragTrigger.callback.AddListener(OnDragBegin);
        dragTrigger.callback.AddListener(OnDrag);
        endDragTrigger.callback.AddListener(OnDragEnd);
        eventTrigger.triggers.Add(beginDragTrigger);
        eventTrigger.triggers.Add(dragTrigger);
        eventTrigger.triggers.Add(endDragTrigger);
    }

    public void OnDragBegin(BaseEventData data)
    {
        PlayerController.Instance.DragBegin();
    }

    public void OnDrag(BaseEventData data)
    {
        Vector3 Pos = Util.Instance.mousePosition;

        stickVec = (Pos - transform.position).normalized;

        float Dis = Vector2.Distance(Pos, transform.position);

        if (Dis < radius)
            stick.position = transform.position + stickVec * Dis;
        else
            stick.position = transform.position + stickVec * radius;

        if(joystickType==JoystickType.Move)
        {
            PlayerController.Instance.Drag();
        }
    }

    public void OnDragEnd(BaseEventData data)
    {
        var power = Vector2.Distance(transform.position, Util.Instance.mousePosition);
        var angle = transform.position - Util.Instance.mousePosition;
        angle /= angle.magnitude;
        PlayerController.Instance.DragEnd(power, angle);
        stick.position = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestJoyStick : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] float radius;
    [SerializeField] bool isMoveStick;
    private Transform stick;
    private Vector3 stickVec;
    public JoystickType joystickType;
    EventTrigger eventTrigger;

    private void Start()
    {
        stick = transform.GetChild(0);
        eventTrigger = stick.GetComponent<EventTrigger>();

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

        stickVec = (Pos - transform.position).normalized;

        float Dis = Vector2.Distance(Pos, transform.position);

        if (Dis < radius)
            stick.position = transform.position + stickVec * Dis;
        else
            stick.position = transform.position + stickVec * radius;

        if (isMoveStick)
        {
            player.Drag();
        }
    }

    public void OnDragEnd(BaseEventData data)
    {
        if (isMoveStick)
        {
            var power = Vector2.Distance(transform.position, Util.Instance.mousePosition);
            var angle = transform.position - Util.Instance.mousePosition;
            angle /= angle.magnitude;

            player.DragEnd(joystickType, power, angle);
        }
        stick.position = transform.position;
    }

}

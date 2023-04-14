using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainTestJoyStick : MonoBehaviour
{
    [SerializeField] float radius;
    public JoystickType joystickType;
    private Transform stick;
    private CameraMove cameraMove;
    private Vector3 stickVec;
    private Vector2 angle;
    private float zAngle;
    EventTrigger eventTrigger;

    public bool isDraging { get; private set; }

    private void Start()
    {
        stick = transform.GetChild(0);
        eventTrigger = stick.GetComponent<EventTrigger>();
        cameraMove = FindObjectOfType<CameraMove>();

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
        isDraging = true;
        MainTestModeManager.Instance.DragBegin();
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

        var v = stick.position - transform.position;
        zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        MainTestModeManager.Instance.Drag(zAngle);

        if (joystickType == JoystickType.Move)
            cameraMove.MoveDrag(-v * 3);
    }

    public void OnDragEnd(BaseEventData data)
    {
        isDraging = false;
        var power = Vector2.Distance(transform.position, Util.Instance.mousePosition);
        angle = transform.position - Util.Instance.mousePosition;
        angle /= angle.magnitude;
        MainTestModeManager.Instance.DragEnd(power, angle);
        cameraMove.MoveDrag(Vector3.zero);
        stick.position = transform.position;
    }
}

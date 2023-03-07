using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGround : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private Vector3 startPos;
    EventTrigger eventTrigger;

    private void Start()
    {
        eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry clickTrigger = new EventTrigger.Entry { eventID = EventTriggerType.BeginDrag };
        EventTrigger.Entry dragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
        EventTrigger.Entry endDragTrigger = new EventTrigger.Entry { eventID = EventTriggerType.EndDrag };
        clickTrigger.callback.AddListener(BeginDrag);
        dragTrigger.callback.AddListener(Drag);
        eventTrigger.triggers.Add(clickTrigger);
        eventTrigger.triggers.Add(dragTrigger);
        eventTrigger.triggers.Add(endDragTrigger);
    }

    private void BeginDrag(BaseEventData data)
    {
        CameraMove.Instance.ResetTarget();
        PlayerController.Instance.ResetSellect();
        startPos.x = Util.Instance.mousePosition.x;
        startPos.y = Util.Instance.mousePosition.y;
    }
    private void Drag(BaseEventData data)
    {
        Vector3 angle = startPos - Util.Instance.mousePosition;
        angle.z = 0;
        Util.Instance.mainCam.transform.position += angle;
    }
}

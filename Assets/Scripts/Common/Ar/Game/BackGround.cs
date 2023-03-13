using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

public class BackGround : MonoBehaviour
{
    [SerializeField] Transform cameraMove;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject roundCinemachine;
    [SerializeField] GameObject hardCinemachine;
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
        endDragTrigger.callback.AddListener(EndDrag);
        eventTrigger.triggers.Add(clickTrigger);
        eventTrigger.triggers.Add(dragTrigger);
        eventTrigger.triggers.Add(endDragTrigger);
    }

    private void BeginDrag(BaseEventData data)
    {
        CameraMove.Instance.ResetTarget();
        PlayerController.Instance.ResetSellect();
        PlayerController.Instance.DisableQuickSlots();
        startPos.x = Util.Instance.mousePosition.x;
        startPos.y = Util.Instance.mousePosition.y;
        roundCinemachine.SetActive(false);
        hardCinemachine.SetActive(true);
    }
    private void Drag(BaseEventData data)
    {
        Vector3 angle = startPos - Util.Instance.mousePosition;
        angle.z = 0;
        cameraMove.position += angle;
    }

    private void EndDrag(BaseEventData data)
    {
        roundCinemachine.SetActive(true);
        hardCinemachine.SetActive(false);
    }
}

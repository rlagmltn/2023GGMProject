using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicJoystick : MonoBehaviour
{
    [SerializeField] JoyStick joyStick;

    private void OnMouseDown()
    {
        if (joyStick.joystickType == JoystickType.None || TurnManager.Instance.SomeoneIsMoving || EventSystem.current.IsPointerOverGameObject()) return;
        joyStick.gameObject.SetActive(true);
        joyStick.OnDragBegin();
        joyStick.transform.position = Util.Instance.mousePosition;
    }
}

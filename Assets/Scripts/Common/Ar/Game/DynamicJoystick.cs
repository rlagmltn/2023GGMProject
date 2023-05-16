using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicJoystick : MonoBehaviour
{
    [SerializeField] JoyStick joyStick;

    private void OnMouseDown()
    {
        if (joyStick.joystickType == JoystickType.None || TurnManager.Instance.SomeoneIsMoving) return;
        Debug.Log("실행히히");
        joyStick.gameObject.SetActive(true);
        joyStick.OnDragBegin();
        joyStick.transform.position = Util.Instance.mousePosition;
    }
}

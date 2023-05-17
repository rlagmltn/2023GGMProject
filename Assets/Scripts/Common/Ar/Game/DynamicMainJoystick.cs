using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMainJoystick : MonoBehaviour
{
    [SerializeField] MainTestJoyStick joyStick;

    private void OnMouseDown()
    {
        if (joyStick.joystickType == JoystickType.None || TurnManager.Instance.SomeoneIsMoving) return;
        joyStick.gameObject.SetActive(true);
        joyStick.OnDragBegin();
        joyStick.transform.position = Util.Instance.mousePosition;    
    }
}

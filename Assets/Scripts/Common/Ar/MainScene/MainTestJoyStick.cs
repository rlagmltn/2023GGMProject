using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class MainTestJoyStick : MonoBehaviour
{
    [SerializeField] float cancelRadius;
    [SerializeField] float radius;
    public JoystickType joystickType;
    private Transform stick;
    private CameraMove cameraMove;
    private Vector3 stickVec;
    private Vector2 angle;
    private float zAngle;

    public bool isDraging { get; private set; }

    private void Start()
    {
        stick = transform.GetChild(1);
        cameraMove = FindObjectOfType<CameraMove>();
    }

    private void Update()
    {
        OnDrag();
    }

    public void OnDragBegin()
    {
        if (TurnManager.Instance.SomeoneIsMoving) return;
        isDraging = true;
        MainTestModeManager.Instance.DragBegin();
    }

    private void OnDrag()
    {
        if (TurnManager.Instance.SomeoneIsMoving || !isDraging) return;
        Vector3 Pos = Util.Instance.mousePosition;

        stickVec = (Pos - transform.position).normalized;

        float Dis = Vector2.Distance(Pos, transform.position);

        if (Dis < radius)
            stick.position = transform.position + stickVec * Dis;
        else
            stick.position = transform.position + stickVec * radius;

        var v = stick.position - transform.position;
        var vDis = Vector2.Distance(stick.position, transform.position) - cancelRadius;

        if (Dis < cancelRadius)
        {
            MainTestModeManager.Instance.TestPlayer.DisableRanges();
            v = Vector2.zero;
            vDis = 0;
        }
        else
            MainTestModeManager.Instance.TestPlayer.ActiveRange().SetActive(true);

        zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        MainTestModeManager.Instance.Drag(zAngle, vDis);

        cameraMove.MoveDrag(new Vector2(-v.x * 0.5f, -v.y * 0.36f));
        cameraMove.ApplyCameraSize(1, vDis * 0.8f);

        if (Input.GetMouseButtonUp(0)) OnDragEnd();
    }

    private void OnDragEnd()
    {
        if (TurnManager.Instance.SomeoneIsMoving || !isDraging) return;
        isDraging = false;
        var power = Vector2.Distance(stick.position, transform.position);
        stick.position = transform.position;
        joystickType = JoystickType.None;

        angle = transform.position - Util.Instance.mousePosition;
        angle /= angle.magnitude;
        var canShoot = false;
        if (power < cancelRadius) canShoot = false;
        else canShoot = true;
        MainTestModeManager.Instance.DragEnd(power - cancelRadius, angle, canShoot);
        cameraMove.MoveDrag(Vector3.zero);
        cameraMove.ApplyCameraSize();

        gameObject.SetActive(false);
    }
}

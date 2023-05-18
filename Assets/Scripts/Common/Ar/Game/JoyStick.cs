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
    [SerializeField] float cancelRadius;
    [SerializeField] float radius;
    public JoystickType joystickType;
    private Transform stick;
    private CameraMove cameraMove;
    private Vector3 stickVec;
    private Vector2 angle;
    private float zAngle;
    private PlayerController playerController;

    public bool isDraging { get; private set; }

    private void Start()
    {
        Started();
    }

    public void Started()
    {
        stick = transform.GetChild(1);
        cameraMove = FindObjectOfType<CameraMove>();
        playerController = FindObjectOfType<PlayerController>();
        joystickType = JoystickType.None;
    }

    private void Update()
    {
        OnDrag();
    }

    public void OnDragBegin()
    {
        if (TurnManager.Instance.SomeoneIsMoving) return;
        isDraging = true;
        playerController.DragBegin(joystickType);
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
            playerController.sellectPlayer.DisableRanges();
            v = Vector2.zero;
            vDis = 0;
        }
        else
            playerController.sellectPlayer.ActiveRange().SetActive(true);

        zAngle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        playerController.Drag(zAngle, vDis);

        cameraMove.MoveDrag(new Vector2(-v.x * 0.5f, -v.y * 0.36f));
        cameraMove.ApplyCameraSize(1, vDis * 0.8f);

        if (Input.GetMouseButtonUp(0)) OnDragEnd();
    }

    public void OnDragEnd()
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
        playerController.DragEnd(power - cancelRadius, angle, canShoot);
        cameraMove.MoveDrag(Vector3.zero);
        cameraMove.ApplyCameraSize();

        gameObject.SetActive(false);
    }
}

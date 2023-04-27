using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;

    private Touch touchZero;
    private Touch touchOne;

    #region 카메라 무브
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 minSize;
    [SerializeField] private Vector2 maxSize;

    private Vector3 moveDragPos;
    private Vector3 setCamPos = new Vector3(0, 0, -10);

    bool moveMove = false;
    float moveSpeed = 10f;
    #endregion

    #region 카메라 셰이크
    private float shakeTime;
    private bool timeIsGoing = false;
    #endregion

    #region 카메라 줌인
    [SerializeField] private float minOrthographicSize = 10;
    [SerializeField] private float maxOrthographicSize = 30;

    private float defaultOrthographicSize = 10f;
    private float orthographicSize;
    public bool isZoommode { get; set; }
    public bool isDragmode { get; set; }
    public bool isBatchmode { get; set; }
    #endregion

    private CinemachineBasicMultiChannelPerlin camNoise;
    private CinemachineTransposer camTransposer;

    [SerializeField] private float moveSensitivity;
    [SerializeField] private float zoomSensitivity;

    private PlayerController playerController;

    public float OrthographicSize { get { return cinemachineCam.m_Lens.OrthographicSize; } }

    private void Start()
    {
        orthographicSize = cinemachineCam.m_Lens.OrthographicSize;
        camNoise = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        camTransposer = cinemachineCam.GetCinemachineComponent<CinemachineTransposer>();
        playerController = FindObjectOfType<PlayerController>();
        SetDefaultZoom();
    }

    private void Update()
    {
        DragMove();
        ChaseTarget();
        ZoomInAndOut();
        KeyboardMove();
    }

    private void FixedUpdate()
    {
        TimeIsGoing();
    }

    private void ChaseTarget()
    {
        if (target != null)
        {
            transform.position = target.position + moveDragPos + setCamPos;
        }
    }

    public void MovetoTarget(Transform target)
    {
        this.target = target.transform;
        EndDrag();
    }

    public void ResetTarget()
    {
        target = null;
    }

    private void ZoomInAndOut()
    {
        if (!TurnManager.Instance.IsPlayerTurn) return;
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            if (isBatchmode) return;
            isZoommode = true;
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;


            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            orthographicSize = Mathf.Clamp(orthographicSize + deltaMagnitudeDiff * zoomSensitivity, minOrthographicSize, maxOrthographicSize);
            ApplyCameraSize();
        }
        else isZoommode = false;
    }

    public void ApplyCameraSize(float zoomScale = 1, float zoomAdd = 0)
    {
        zoomScale = Mathf.Clamp(zoomScale * 2, 0.3f, 1);
        zoomAdd *= (orthographicSize / 6);
        cinemachineCam.m_Lens.OrthographicSize = Mathf.Clamp(orthographicSize * zoomScale + zoomAdd, minOrthographicSize / 2, maxOrthographicSize * 1.5f);
    }

    public void MoveDrag(Vector3 dragPos)
    {
        moveDragPos = dragPos * orthographicSize;
    }

    public void Shake(float amplitude = 12, float duration = 0.3f)
    {
        timeIsGoing = true;

        if(duration>shakeTime)
            shakeTime = duration;

        if(amplitude> camNoise.m_AmplitudeGain)
            camNoise.m_AmplitudeGain = amplitude;
    }

    private void TimeIsGoing()
    {
        if (!timeIsGoing) return;
        shakeTime -= Time.deltaTime;
        if(shakeTime < 0)
        {
            camNoise.m_AmplitudeGain = 0;
            timeIsGoing = false;
        }
    }

    public void StopShake()
    {
        shakeTime = 0;
    }

    private void DragMove()
    {
        if (!TurnManager.Instance.IsPlayerTurn || isZoommode) return;
        if (Input.touchCount == 1)
        {
            touchZero = Input.GetTouch(0);
            if (!isDragmode && !isBatchmode) BeginDrag();
            else Drag();
        }
        else if (Input.touchCount == 0) EndDrag();
    }

    private void KeyboardMove()
    {
        var dir = Vector3.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.y = Input.GetAxisRaw("Vertical");

        if(dir!=Vector3.zero && !moveMove)
        {
            moveMove = true;
            ResetTarget();
            camTransposer.m_XDamping = 0;
            camTransposer.m_YDamping = 0;
            camTransposer.m_ZDamping = 0;
        }
        else if(moveMove)
        {
            moveMove = false;
            camTransposer.m_XDamping = 0.5f;
            camTransposer.m_YDamping = 0.5f;
            camTransposer.m_ZDamping = 0.5f;
        }
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Minus))
        {
            orthographicSize = Mathf.Clamp(orthographicSize+1, minOrthographicSize, maxOrthographicSize);
            ApplyCameraSize();
        }
        else if (Input.GetKeyDown(KeyCode.Equals))
        {
            orthographicSize = Mathf.Clamp(orthographicSize - 1, minOrthographicSize, maxOrthographicSize);
            ApplyCameraSize();
        }
    }

    private void BeginDrag()
    {
        if (touchZero.deltaPosition.magnitude < 5) return;
        if (EventSystem.current.IsPointerOverGameObject(touchZero.fingerId))
            isBatchmode = true;
        else
        {
            isDragmode = true;
            ResetTarget();
            playerController.ResetSellect();
            playerController.DisableQuickSlots();
            camTransposer.m_XDamping = 0;
            camTransposer.m_YDamping = 0;
            camTransposer.m_ZDamping = 0;
        }
    }
    private void Drag()
    {
        if (!isDragmode) return;
        Vector3 pos = touchZero.deltaPosition * moveSensitivity;
        transform.position -= pos;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, minSize.x, maxSize.x), Mathf.Clamp(transform.position.y, minSize.y, maxSize.y));
    }

    private void EndDrag()
    {
        camTransposer.m_XDamping = 0.5f;
        camTransposer.m_YDamping = 0.5f;
        camTransposer.m_ZDamping = 0.5f;
        isDragmode = false;
        isBatchmode = false;
    }

    public void TimeFreeze(float amount = 1)
    {
        if (amount < 0.3f) StopShake();
        Time.timeScale = Mathf.Clamp(amount, 0.1f, 1f);
    }

    public void SetDefaultZoom()
    {
        orthographicSize = defaultOrthographicSize;
        ApplyCameraSize();
    }
}

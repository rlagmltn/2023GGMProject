using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMove : MonoSingleton<CameraMove>
{
    [SerializeField] private Transform target;

    [SerializeField] private CinemachineVirtualCamera shakeCam;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;
    [SerializeField] private float duration;

    [SerializeField] private float zoomAmount;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float minOrthographicSize = 10;
    [SerializeField] private float maxOrthographicSize = 30;

    private CinemachineBasicMultiChannelPerlin shakeCamNoise;
    private Vector3 setCamPos = new Vector3(0, 0, -10);
    private Vector3 moveDragPos;

    private float defaultOrthographicSize = 4.5f;
    private float orthographicSize;
    private float targetOrthographicSize;

    private void Awake()
    {
        Util.Instance.mainCam.transform.position = setCamPos;
        orthographicSize = shakeCam.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    public void MovetoTarget(Transform target)
    {
        this.target = target.transform;
        transform.position += setCamPos;
    }

    private void Start()
    {
        shakeCamNoise = shakeCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        ChaseTarget();
        if (!TurnManager.Instance.IsPlayerTurn) return;
        ZoomInAndOut();
        HandleZoom();
    }

    public void ChaseTarget()
    {
        if (target != null)
        {
            transform.position = target.position + moveDragPos;
            transform.position += setCamPos;
        }
    }

    private void ZoomInAndOut()
    {
        if (Input.touchCount == 2) //�հ��� 2���� ������ ��
        {
            Touch touchZero = Input.GetTouch(0); //ù��° �հ��� ��ġ�� ����
            Touch touchOne = Input.GetTouch(1); //�ι�° �հ��� ��ġ�� ����

            //��ġ�� ���� ���� ��ġ���� ���� ������
            //ó�� ��ġ�� ��ġ(touchZero.position)���� ���� �����ӿ����� ��ġ ��ġ�� �̹� �����ӿ��� ��ġ ��ġ�� ���̸� ��
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition�� �̵����� ������ �� ���
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // �� �����ӿ��� ��ġ ������ ���� �Ÿ� ����
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude�� �� ������ �Ÿ� ��(����)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // �Ÿ� ���� ����(�Ÿ��� �������� ũ��(���̳ʽ��� ������)�հ����� ���� ����_���� ����)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            shakeCam.m_Lens.OrthographicSize += deltaMagnitudeDiff * zoomAmount;
            shakeCam.m_Lens.OrthographicSize = Mathf.Max(shakeCam.m_Lens.OrthographicSize, 0.1f);
        }
    }

    private void HandleZoom()
    {
        targetOrthographicSize -= Input.mouseScrollDelta.y * zoomAmount;

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        shakeCam.m_Lens.OrthographicSize = orthographicSize;
    }

    public void ResetTarget()
    {
        target = null;
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    public void MoveDrag(Vector3 dragPos)
    {
        moveDragPos = dragPos;
    }

    private IEnumerator ShakeCoroutine()
    {
        shakeCamNoise.m_AmplitudeGain = amplitude;
        shakeCamNoise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        shakeCamNoise.m_AmplitudeGain = 0;
        shakeCamNoise.m_FrequencyGain = 0;
    }

    public void TimeFreeze(float amount)
    {
        Time.timeScale = amount;
        //StartCoroutine(TimeFreezeCoru(amount, duration));
    }
    
    public void EffectZoom(float amount)
    {
        orthographicSize -= (1 - amount)*2;
    }

    public void SetDefaultZoom()
    {
        shakeCam.m_Lens.OrthographicSize = defaultOrthographicSize;
    }
}

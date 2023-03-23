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
    [SerializeField] private float zoomMul;

    private CinemachineBasicMultiChannelPerlin shakeCamNoise;
    private Vector3 setCamPos = new Vector3(0, 0, -10);
    private Vector3 moveDragPos;

    private void Awake()
    {
        Util.Instance.mainCam.transform.position = setCamPos;
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
        ZoomInAndOut();
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

            shakeCam.m_Lens.OrthographicSize += deltaMagnitudeDiff * zoomMul;
            shakeCam.m_Lens.OrthographicSize = Mathf.Max(shakeCam.m_Lens.OrthographicSize, 0.1f);
        }
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
}

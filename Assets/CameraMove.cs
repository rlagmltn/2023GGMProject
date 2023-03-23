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
        if (Input.touchCount == 2) //손가락 2개가 눌렸을 때
        {
            Touch touchZero = Input.GetTouch(0); //첫번째 손가락 터치를 저장
            Touch touchOne = Input.GetTouch(1); //두번째 손가락 터치를 저장

            //터치에 대한 이전 위치값을 각각 저장함
            //처음 터치한 위치(touchZero.position)에서 이전 프레임에서의 터치 위치와 이번 프로임에서 터치 위치의 차이를 뺌
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition; //deltaPosition는 이동방향 추적할 때 사용
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 각 프레임에서 터치 사이의 벡터 거리 구함
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude; //magnitude는 두 점간의 거리 비교(벡터)
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 거리 차이 구함(거리가 이전보다 크면(마이너스가 나오면)손가락을 벌린 상태_줌인 상태)
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

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

    private CinemachineBasicMultiChannelPerlin shakeCamNoise;
    private Vector3 setCamPos = new Vector3(0, 0, -10);

    private void Awake()
    {
        Util.Instance.mainCam.transform.position = setCamPos;
    }

    public void MovetoTarget(Player target)
    {
        this.target = target.transform;
        transform.position += setCamPos;
    }

    private void Start()
    {
        shakeCamNoise = shakeCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        ChaseTarget();  
    }

    public void ChaseTarget()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.position += setCamPos;
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

    private IEnumerator ShakeCoroutine()
    {
        shakeCamNoise.m_AmplitudeGain = amplitude;
        shakeCamNoise.m_FrequencyGain = frequency;
        yield return new WaitForSeconds(duration);
        shakeCamNoise.m_AmplitudeGain = 0;
        shakeCamNoise.m_FrequencyGain = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoSingleton<CameraMove>
{
    [SerializeField] private Transform target;

    private void Awake()
    {
        Util.Instance.mainCam.transform.position = new Vector3(0, 0, -10);
    }

    public void MovetoTarget(Player target)
    {
        this.target = target.transform;
        transform.position += new Vector3(0, 0, -10);
    }

    private void Start()
    {
        
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
            transform.position += new Vector3(0, 0, -10);
        }
    }

    public void ResetTarget()
    {
        target = null;
    }
}

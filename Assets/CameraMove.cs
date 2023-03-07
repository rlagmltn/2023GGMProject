using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoSingleton<CameraMove>
{
    private Transform target;

    public void MovetoTarget(Player target)
    {
        transform.DOMoveX(target.transform.position.x, 0.3f);
        transform.DOMoveY(target.transform.position.y, 0.3f);
        StartCoroutine(SetTarget(target.transform));
        transform.position += new Vector3(0, 0, -10);
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

    private IEnumerator SetTarget(Transform _target)
    {
        target = null;
        yield return new WaitForSeconds(0.31f);
        target = _target;
    }

    public void ResetTarget()
    {
        target = null;
    }
}

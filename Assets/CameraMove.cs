using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoSingleton<CameraMove>
{
    private Transform target;
    Coroutine co;

    public void MovetoTarget(Player target)
    {
        if(co!=null) StopCoroutine(co);
        transform.DOMoveX(target.transform.position.x, 0.3f);
        transform.DOMoveY(target.transform.position.y, 0.3f);
        co = StartCoroutine(SetTarget(target.transform));
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
        yield return new WaitForSeconds(0.3f);
        target = _target;
    }

    public void ResetTarget()
    {
        target = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoSingleton<CameraMove>
{
    public void MovetoTarget(Vector2 targetPos)
    {
        transform.DOMoveX(targetPos.x, 0.3f);
        transform.DOMoveY(targetPos.y, 0.3f);
        transform.position += new Vector3(0, 0, -10);
    }
}

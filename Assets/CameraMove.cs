using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoSingleton<CameraMove>
{
    public void MovetoTarget(Vector2 targetPos)
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, 0.9f);
        transform.position += new Vector3(0, 0, -10);
    }
}

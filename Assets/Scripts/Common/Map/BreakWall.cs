using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        EffectManager.Instance.InstantiateEffect(Effect.WALLCRUSH, collision.collider.ClosestPoint(transform.position), Vector2.zero);
    }
}

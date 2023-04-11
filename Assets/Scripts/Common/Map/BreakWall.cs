using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject particle = EffectManager.Instance.InstantiateEffect(Effect.WALLCRUSH, transform.position, Vector2.zero);
        particle.transform.position = collision.collider.ClosestPoint(transform.position);
        Destroy(gameObject);
    }
}

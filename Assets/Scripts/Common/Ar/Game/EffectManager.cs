using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    [SerializeField] GameObject[] effects;

    public GameObject InstantiateEffect(int num, Vector3 pos, Vector2 start, Vector2 end)
    {
        var angle = end - start;
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var effect = Instantiate(effects[num], pos, Quaternion.Euler(0, 0, zAngle - 90));

        return effect;
    }
}

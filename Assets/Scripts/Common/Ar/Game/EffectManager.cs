using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    [SerializeField] GameObject[] effects;

    public GameObject InstantiateEffect(int num, Vector3 pos, float zAngle)
    {
        var effect = Instantiate(effects[num], pos, Quaternion.Euler(0, 0, zAngle));

        return effect;
    }
}

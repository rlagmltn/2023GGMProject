using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    [SerializeField] GameObject[] effects;
    [SerializeField] FloatDamage floatDamage;

    public GameObject InstantiateEffect(Effect num, Vector3 pos, Vector2 start, Vector2 end)
    {
        var angle = end - start;
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var effect = Instantiate(effects[(int)num], pos, Quaternion.Euler(0, 0, zAngle));

        return effect;
    }
    public GameObject InstantiateEffect(Effect num, Vector3 pos, Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var effect = Instantiate(effects[(int)num], pos, Quaternion.Euler(0, 0, zAngle));

        return effect;
    }

    public GameObject InstantiateEffect_P(Effect num, Vector3 pos, Vector2 angle)
    {
        var particle = effects[(int)num].GetComponentInChildren<ParticleSystem>();
        var ptMain = particle.main;
        ptMain.startRotation = Mathf.Atan2(angle.y, angle.x);
        var effect = Instantiate(particle.transform.parent.gameObject, pos, Quaternion.Euler(0, 0, 0));

        return effect;
    }

    public FloatDamage InstantiateFloatDamage(Vector3 pos)
    {
        var damage = Instantiate(floatDamage, pos, Quaternion.identity);
        return damage;
    }
}

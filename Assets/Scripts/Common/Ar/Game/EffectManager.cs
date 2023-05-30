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
        var pAngle = Mathf.Atan2(angle.y, angle.x);
        if (pAngle > -1.5f && pAngle < 1.5f)
        {
            particle.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            particle.transform.localScale = new Vector3(1, -1, 1);
            pAngle *= -1;
        }
        ptMain.startRotation = pAngle;
        var effect = Instantiate(particle.transform.parent.gameObject, pos, Quaternion.Euler(0, 0, 0));

        return effect;
    }

    public GameObject InstantiateEffect_P(Effect num, Vector3 pos)
    {
        var particle = effects[(int)num].GetComponentInChildren<ParticleSystem>();
        var effect = Instantiate(particle.transform.parent.gameObject, pos, Quaternion.Euler(0, 0, 0));

        return effect;
    }

    public FloatDamage InstantiateFloatDamage(Vector3 pos)
    {
        var damage = Instantiate(floatDamage, pos, Quaternion.identity);
        return damage;
    }
}

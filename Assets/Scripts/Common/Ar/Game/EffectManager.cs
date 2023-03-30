using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoSingleton<EffectManager>
{
    public enum Effect : int
    {
        CRUSH1,
        CRUSH2,
        SMASH,
        MOVE
    }

    [SerializeField] Object[] effects;

    private void Awake()
    {
        effects = Resources.LoadAll("Sprites/Effect/Prefabs");
    }

    public GameObject InstantiateEffect(int num, Vector3 pos, Vector2 start, Vector2 end)
    {
        var angle = end - start;
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var effect = Instantiate(effects[num], pos, Quaternion.Euler(0, 0, zAngle - 90)) as GameObject;

        return effect;
    }
    public GameObject InstantiateEffect(int num, Vector2 pos, Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var effect = Instantiate(effects[num], pos, Quaternion.Euler(0, 0, zAngle)) as GameObject;

        return effect;
    }
}

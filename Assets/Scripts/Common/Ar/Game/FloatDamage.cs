using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FloatDamage : MonoBehaviour
{
    private TextMeshPro tmp;

    public void DamageText(int damage)
    {
        Destroy(gameObject, 0.8f);
        if (tmp == null) tmp = GetComponent<TextMeshPro>();
        tmp.SetText(damage.ToString());
        tmp.fontSize = 12 + damage / (float)6;
        transform.DOMoveY(transform.position.y + Random.Range(1.5f, 2.5f), 0.8f);
        transform.DOMoveX(transform.position.x + Random.Range(-0.8f, 0.8f), 0.8f);
    }
}

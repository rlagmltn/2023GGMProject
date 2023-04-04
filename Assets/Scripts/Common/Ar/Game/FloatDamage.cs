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
        tmp.fontSize = 6 + damage/5;
        transform.DOMoveY(transform.position.y + 2f, 0.8f);
    }
}

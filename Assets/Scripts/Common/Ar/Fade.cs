using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    [SerializeField] Image image;

    private void Start()
    {
        image.enabled = true;
        image.DOColor(new Color(0, 0, 0, 0), 1f).OnComplete(()=> { Destroy(gameObject); });
    }
}

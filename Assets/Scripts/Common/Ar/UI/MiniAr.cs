using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniAr : MonoBehaviour
{
    private Ar targetAr;
    private Image image;

    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        if (!targetAr.gameObject.activeSelf) gameObject.SetActive(false);
        transform.localPosition = targetAr.transform.position*MiniMap.Instance.GetMiniMapRatio();
    }

    public void SetTarget(Ar ar)
    {
        targetAr = ar;
        image = GetComponent<Image>();

        if (targetAr.CompareTag("Player")) image.color = Color.blue;
        else if (targetAr.CompareTag("Enemy")) image.color = Color.red;
        else image.color = Color.black;
    }
}

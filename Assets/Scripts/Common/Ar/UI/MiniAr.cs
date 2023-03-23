using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniAr : MonoBehaviour
{
    private Ar targetAr;
    private Image image;
    private RectTransform range;

    private Transform activeRange;

    private void Update()
    {
        UpdatePos();
    }

    private void UpdatePos()
    {
        if (!targetAr.gameObject.activeSelf) gameObject.SetActive(false);
        transform.localPosition = targetAr.transform.position*MiniMap.Instance.GetMiniMapRatio();
        if(activeRange!=null) transform.rotation = activeRange.rotation;
    }

    public void SetTarget(Ar ar)
    {
        targetAr = ar;
        image = GetComponent<Image>();
        range = transform.GetChild(0).GetComponent<RectTransform>();
        range.gameObject.SetActive(false);

        if (targetAr.CompareTag("Player")) image.color = Color.blue;
        else if (targetAr.CompareTag("Enemy")) image.color = Color.red;
        else image.color = Color.black;

        targetAr.GetComponent<Player>()?.SetMini(this);
    }

    public void ShowRange(GameObject obj)
    {
        range.gameObject.SetActive(true);
        range.sizeDelta = obj.transform.localScale * 20;
        range.transform.localPosition = obj.transform.localPosition * 20;
        activeRange = obj.transform;
    }

    public void DisableRange()
    {
        range.gameObject.SetActive(false);
        activeRange = null;
    }
}

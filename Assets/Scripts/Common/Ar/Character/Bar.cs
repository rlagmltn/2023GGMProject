using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private SpriteRenderer gage;
    private Image iGage;
    private float gageAmount = 1;
    private bool isGageChanged = false;

    private void Start()
    {
        Set();
    }

    private void Set()
    {
        gage = transform.GetChild(0).GetComponent<SpriteRenderer>();
        iGage = transform.GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        GageValueChange();
    }

    private void GageValueChange()
    {
        if (!isGageChanged) return;
        if(gage!=null)
        {
            if (gage.size.x <= gageAmount)
            {
                isGageChanged = false;
                return;
            }
            gage.size = new Vector2(gage.size.x - Time.deltaTime, 1);
        }
        else
        {
            if (iGage.fillAmount <= gageAmount)
            {
                isGageChanged = false;
                return;
            }
            iGage.fillAmount = iGage.fillAmount - Time.deltaTime;
        }
    }

    public void GageChange(float gageAmount)
    {
        if (gage == null && iGage == null) Set();
        this.gageAmount = gageAmount;
        isGageChanged = true;
    }
}

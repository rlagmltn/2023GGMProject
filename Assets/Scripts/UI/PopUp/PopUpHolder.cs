using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpHolder : MonoBehaviour
{
    private Transform PopUpObj;

    public void SetPopUpObj(Transform trnasform)
    {
        PopUpObj = trnasform;
        PopUpObj.gameObject.SetActive(false);
    }

    public Transform ReturnPopUpObj()
    {
        return PopUpObj;
    }

    public void PopUpUI()
    {
        PopUpObj.gameObject.SetActive(true);
    }

    public void PopDownUI()
    {
        PopUpObj.gameObject.SetActive(false);
    }
}

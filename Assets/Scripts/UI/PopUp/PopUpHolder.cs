using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PopUp창을 저장(Hold)시키는 함수
/// </summary>
public class PopUpHolder : MonoBehaviour
{
    private Transform PopUpObj;

    /// <summary>
    /// 팝업 오브젝트를 저장하는 함수
    /// </summary>
    /// <param name="trnasform"></param>
    public void SetPopUpObj(Transform trnasform)
    {
        PopUpObj = trnasform;
        PopUpObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// 팝업 오브젝트를 리턴시켜줌
    /// </summary>
    /// <returns></returns>
    public Transform ReturnPopUpObj()
    {
        return PopUpObj;
    }

    /// <summary>
    /// UI를 PopUp시키는 함수
    /// </summary>
    public void PopUpUI()
    {
        PopUpObj.gameObject.SetActive(true);
    }

    /// <summary>
    /// UI를 PopDown시키는 함수
    /// </summary>
    public void PopDownUI()
    {
        PopUpObj.gameObject.SetActive(false);
    }
}

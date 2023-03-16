using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PopUpâ�� ����(Hold)��Ű�� �Լ�
/// </summary>
public class PopUpHolder : MonoBehaviour
{
    private Transform PopUpObj;

    /// <summary>
    /// �˾� ������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="trnasform"></param>
    public void SetPopUpObj(Transform trnasform)
    {
        PopUpObj = trnasform;
        PopUpObj.gameObject.SetActive(false);
    }

    /// <summary>
    /// �˾� ������Ʈ�� ���Ͻ�����
    /// </summary>
    /// <returns></returns>
    public Transform ReturnPopUpObj()
    {
        return PopUpObj;
    }

    /// <summary>
    /// UI�� PopUp��Ű�� �Լ�
    /// </summary>
    public void PopUpUI()
    {
        PopUpObj.gameObject.SetActive(true);
    }

    /// <summary>
    /// UI�� PopDown��Ű�� �Լ�
    /// </summary>
    public void PopDownUI()
    {
        PopUpObj.gameObject.SetActive(false);
    }
}

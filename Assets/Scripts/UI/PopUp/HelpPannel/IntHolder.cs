using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// int값을 저장(Hold)시키는 함수
/// </summary>
public class IntHolder : MonoBehaviour
{
    private int _num;

    public void SetNum(int num)
    {
        _num = num;
    }

    public void GetNum_Help()
    {
    }
}

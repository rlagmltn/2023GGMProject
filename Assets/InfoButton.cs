using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    [SerializeField] private int num;

    public int GetInfoNum()
    {
        return num;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item Database", menuName ="Ars/Database")]
public class ArDBObj : ScriptableObject
{
    public ArObj[] arObjects;

    private void OnValidate()
    {
        for(int i = 0; i < arObjects.Length; ++i)
        {
            arObjects[i].arData.ar_id = i;
        }
    }
}

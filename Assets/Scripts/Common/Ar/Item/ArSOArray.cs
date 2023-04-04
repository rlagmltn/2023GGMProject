using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArSOArray", menuName = "SO/ArSOArray")]
public class ArSOArray : ScriptableObject
{
    public ArSOList dataBase;
    public List<ArSO> list;

    public void LoadArlist()
    {
        list.Clear();
        list.AddRange(dataBase.list);
    }

    public void ResetArlist()
    {
        list.Clear();
    }
}

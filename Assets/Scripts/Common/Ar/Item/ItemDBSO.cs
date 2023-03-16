using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ItemDB", menuName = "SO/Item/ItemDBs")]
public class ItemDBSO : ScriptableObject
{
    public ItemSO[] items;
}

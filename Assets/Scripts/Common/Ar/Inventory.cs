using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Slot> invenSlotList;

    private void Start()
    {
        invenSlotList = Encyclopedia.Instance.GetSlotList();
        foreach(Slot slot in invenSlotList)
        {
            var a = Instantiate(slot);
        }
    }
}

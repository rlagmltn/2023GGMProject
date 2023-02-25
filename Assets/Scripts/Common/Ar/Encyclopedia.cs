using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoSingleton<Encyclopedia>
{
    [SerializeField] List<Slot> slotList;

    public List<Slot> GetSlotList()
    {
        return slotList;
    }
}

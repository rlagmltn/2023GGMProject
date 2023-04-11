using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDrop : MonoBehaviour
{
    [SerializeField] private int Num;

    private ArSO Ar;

    internal int GetNum()
    {
        return Num;
    }

    internal void SetCharacterSO(ArSO _AR)
    {
        Ar = _AR;
    }

    internal ArSO GetCharacterSO()
    {
        return Ar;
    }
}

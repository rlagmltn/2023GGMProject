using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoSingleton<MiniMap>
{
    [SerializeField] MiniAr miniAr;
    [SerializeField] float miniMapRatio;
    public Ar[] allAr;

    private void Start()
    {
        allAr = FindObjectsOfType<Ar>();

        foreach(Ar ar in allAr)
        {
            var mini = Instantiate(miniAr, transform);
            mini.SetTarget(ar);
        }
    }

    public float GetMiniMapRatio()
    {
        return miniMapRatio;
    }
}

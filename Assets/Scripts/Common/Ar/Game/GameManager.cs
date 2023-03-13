using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private Transform map;

    private void Start()
    {
        map = Global.Map;
        InstantiateMap();
    }

    void InstantiateMap()
    {
        Instantiate(map);
    }
}

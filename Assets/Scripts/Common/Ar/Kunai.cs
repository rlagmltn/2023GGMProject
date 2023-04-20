using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kunai : Bullet
{
    protected override void SetSO()
    {
        summoner = FindObjectOfType<Assassin>();
        base.SetSO();
    }

    protected override void AfterCrush()
    {
        summoner.transform.position = transform.position;
        summoner.Push(new Vector2(0.1f, 0.1f));
        base.AfterCrush();
    }
}

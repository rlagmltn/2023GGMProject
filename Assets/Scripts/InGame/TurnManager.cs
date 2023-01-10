using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSingleton<TurnManager>
{
    private Ar[] bullets;
    public static int Turn { get; private set; }

    private void Start()
    {
        bullets = FindObjectsOfType<Ar>();
    }

    public void AddTrun(int add = 1)
    {
        Turn += add;
        foreach (Ar bullet in bullets)
            bullet.CoolDown();
    }
}

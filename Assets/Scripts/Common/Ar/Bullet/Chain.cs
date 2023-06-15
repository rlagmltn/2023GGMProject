using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : Bullet
{
    protected override void SetSO()
    {
        base.SetSO();
        summoner = FindObjectOfType<Executioner>();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") || collision.CompareTag("Player"))
        {
            summoner.GetComponent<Executioner>().Grap(collision.GetComponent<Ar>());
        }
        base.OnTriggerEnter2D(collision);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingFlower : ItemInfo
{
    [SerializeField] private float PushAreaSize;
    [SerializeField] private float PushPower;

    public override void Passive()
    {
        GetEnemys();
    }

    void GetEnemys()
    {
        Collider2D[] ray = Physics2D.OverlapCircleAll(player.transform.position, PushAreaSize);

        //¹Ð±â
        foreach(Collider2D col in ray)
        {
            Vector2 vec = col.transform.position - player.transform.position;
            float Add = vec.x + vec.y;
            vec = new Vector2(vec.x / Add, vec.y / Add);
            col.GetComponent<Ar>().Push(vec * PushPower);
        }
    }
}

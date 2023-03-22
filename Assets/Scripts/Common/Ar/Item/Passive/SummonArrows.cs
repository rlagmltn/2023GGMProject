using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonArrows : ItemInfo
{
    [SerializeField] Bullet bullet;
    public override void Passive()
    {
        for(int i=0; i<12; i++)
        {
            Shoot(i*30);
        }
    }

    void Shoot(float angle)
    {
        var _bullet = Instantiate(bullet, player.transform.position, Quaternion.Euler(0, 0, angle - 180));
    }
}

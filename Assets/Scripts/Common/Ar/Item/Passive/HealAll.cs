using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAll : ItemInfo
{
    [SerializeField] float healRadius;
    [SerializeField] int healHP;
    public override void Passive()
    {
        var cols = Physics2D.OverlapCircleAll(player.transform.position, healRadius);

        foreach(Collider2D col in cols)
        {
            var healTarget = col.GetComponent<Player>();
            if(healTarget!=null)
            {
                healTarget.Hit(-5);
                Debug.Log("µýµý µû¶ó´Ü!");
            }
        }
    }
}

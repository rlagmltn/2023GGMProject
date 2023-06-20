using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OminousTalisman_TurnEnd : ItemInfo
{
    public List<Transform> tempTransform;

    public override void Passive()
    {
        OminousTalisman_TurnEnd_Play();
    }

    void OminousTalisman_TurnEnd_Play()
    {
        Debug.Log("½ÇÇà");
        //foreach(Transform trans in player.OminousTalismanDic.Keys)
        //{
        //    int Damage = (int)((float)trans.GetComponent<Ar>().stat.MaxHP / 10f);
        //    if (Damage < 1) Damage = 1;

        //    Debug.Log(Damage); 
        //    trans.GetComponent<Ar>().Hit(Damage);

        //    int temp = player.OminousTalismanDic[trans];
        //    player.OminousTalismanDic[trans] = temp + 1;
        //    //player.OminousTalismanDic[trans] -= 1;

        //    //if (player.OminousTalismanDic[trans] <= 0) tempTransform.Add(trans); 
        //    //player.OminousTalismanDic.Remove(trans);
        //}

        foreach(var Dic in player.OminousTalismanDic.ToList())
        {
            int Damage = (int)((float)Dic.Key.GetComponent<Ar>().stat.MaxHP / 10f);
            if (Damage < 1) Damage = 1;

            Debug.Log(Damage);
            EffectManager.Instance.InstantiateEffect_P(Effect.Blood, Dic.Key.transform.position);
            Dic.Key.GetComponent<Ar>().Hit(Damage, player);
            player.OminousTalismanDic[Dic.Key]--;

            if (player.OminousTalismanDic[Dic.Key] < 1) player.OminousTalismanDic.Remove(Dic.Key);
        }
    }
}

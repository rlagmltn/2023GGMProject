using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBoss : Enemy
{
    private const float defaultWeight = 10f;
    
    private bool pHeavyArmor;
    private int pOverload;

    private int passiveCnt;

    private bool pShield;
    private int shieldCnt;

    [SerializeField] private Transform banditWarrior;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        pHeavyArmor = false;
        pShield = false;
        pOverload = 0;
        passiveCnt = 4;
        stat.WEIGHT = defaultWeight;
        stat.MaxSP = 25;
        stat.MaxHP = 35;
        stat.ATK = 7;
        stat.CriPer = 15;
        stat.CriDmg = 1.5f;

        base.StatReset();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.Z))
        {
            SkillHeavyArmor();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            SkillBleed();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            SkillOverload();
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            SkillSpawn();
        }

    }

    public void Passive()
    {
        if(pOverload > 0)
        {
            pOverload--;
            if(pOverload == 0)
            {
                stat.SP = stat.MaxSP;
                stat.WEIGHT = defaultWeight;
            }
        }

        if(shieldCnt > 0)
        {
            shieldCnt--;
            stat.HP = Mathf.Min(stat.MaxHP, stat.HP + 10);
            stat.ATK += 3;
            pShield = true;
            

        }
        
        passiveCnt--;
        if(stat.SP == 0)
        {
            pHeavyArmor = false;
        }

        if(pHeavyArmor)
        {
            HeavyArmor();
        }

        if(passiveCnt == 0)
        {
            stat.SP += 7;
        }
        
        stat.WEIGHT = defaultWeight + Mathf.Max(stat.SP - stat.MaxSP, 0);
        
    }

    public void SkillHeavyArmor()
    {
        pHeavyArmor = true;
        if(stat.SP > stat.MaxSP)
        {
            stat.SP = stat.MaxSP;
        }
        stat.WEIGHT = defaultWeight;
        

    }

    private void HeavyArmor()
    {
        stat.SP += 3;
    }

    public void SkillOverload()
    {
        pOverload = 2;
        stat.SP = 25;
    }

    public void SkillSpawn()
    {
        for(int i = 0; i<2; i++)
        {
            Vector2 pos = new Vector2(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f));
            Instantiate(banditWarrior, pos, Quaternion.identity);
            
        }
        stat.SP = Mathf.Max(0, stat.SP - 10);
    }

    public void SkillShield()
    {
        stat.SP = 10;
        shieldCnt = 1;
    }

    public void SkillBleed()
    {
        Debug.Log("SkillBleed");
        Player[] players = FindObjectsOfType<Player>();
        if (players.Length == 0)
        {
            Debug.Log("None player");
            return;
        }

        if (players.Length == 1)
        {
            players[0].stat.HP = Mathf.Max(0, players[0].stat.HP-5);
            players[0].transform.Find("HpBack").GetComponent<Bar>().GageChange(players[0].stat.HP / players[0].stat.MaxHP);
            players[0].stat.SP = Mathf.Min(players[0].stat.MaxSP, players[0].stat.SP + 5);
            players[0].transform.Find("DpBack").GetComponent<Bar>().GageChange(players[0].stat.SP / players[0].stat.MaxSP);
        }
        else
        {
            for(int i = 0; i<2; i++)
            {
                players[i].stat.HP = Mathf.Max(0, players[0].stat.HP - 5);
                players[i].transform.Find("HpBack").GetComponent<Bar>().GageChange(players[i].stat.HP / players[i].stat.MaxHP);
                players[i].stat.SP = Mathf.Min(players[i].stat.MaxSP, players[i].stat.SP + 5);
                players[i].transform.Find("DpBack").GetComponent<Bar>().GageChange(players[i].stat.SP / players[i].stat.MaxSP);

                if(players[i].stat.HP == 0)
                {
                    players[i].DeadCheck();
                    

                }
            }
        }
    }

    public bool CanMove()
    {
        if (pOverload > 0 || shieldCnt > 0) return false;

        return true;
    }
}

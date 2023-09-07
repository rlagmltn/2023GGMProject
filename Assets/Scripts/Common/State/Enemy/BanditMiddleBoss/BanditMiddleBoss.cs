using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditMiddleBoss : Enemy
{
    private Transform target;
    private float angleRange = 30f;
    private float radius = 3f;

    private int enemyCnt;
    private int phaseChange;

    [SerializeField] private GameObject axe;
    
    public override void StatReset()
    {
        stat.MaxSP = 20;
        stat.MaxHP = 24;
        stat.ATK = 5;
        stat.CriPer = 20;
        stat.CriDmg = 1.5f;
        enemyCnt = GameManager.Instance.enemies.Length;
        phaseChange = enemyCnt - 3;
        Phase1();
        base.StatReset();
    }

    private void Phase1()
    {
        stat.WEIGHT = 100000;
    }

    private void Phase2()
    {
        stat.WEIGHT = 5;
        stat.SP = stat.MaxSP;
    }

    public void Passive()
    {
        if(GameManager.Instance.enemies.Length < enemyCnt)
        {
            stat.ATK++;
            enemyCnt = GameManager.Instance.enemies.Length;
        }

        if(GameManager.Instance.enemies.Length <= phaseChange)
        {
            Phase2();
        }
        
    }

    private void Howling()
    {
        foreach(Player player in GameManager.Instance.friendly)
        {
            //���� �����
        }
    }

    public void ThrowAxe(Vector2 angle)
    {
        if (axe == null)
        {
            Debug.LogWarning("axeGameObject is Null");
            return;
        }

        float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(axe, transform.position, Quaternion.Euler(0, 0, rangeAngle));
        cameraMove.MovetoTarget(bullet.transform);
    }

    public void AxeAttack()
    {
        Ar[] ars = FindObjectsOfType<Player>();
        Vector3 interV;

        for (int i = 0; i < ars.Length; i++)
        {
            interV = ars[i].transform.position - transform.position;
            if (interV.magnitude <= radius)
            {
                float dot = Vector3.Dot(interV.normalized, transform.forward);
                float theta = Mathf.Acos(dot);
                float degree = Mathf.Rad2Deg * theta;

                if (degree <= angleRange / 2f)
                {
                    BattleManager.Instance.SettingAr(ars[i], axe.GetComponent<Bullet>());
                }
            }
        }
    }
}

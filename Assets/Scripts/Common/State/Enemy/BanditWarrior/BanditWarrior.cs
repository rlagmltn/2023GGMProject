using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditWarrior : Enemy
{
    private Ar lastAr = null;
    private bool canUsePassive = true;

    protected override void Start()
    {
        base.Start();
        BeforeAttack.AddListener(PassiveDP);
        AfterDefence.AddListener(PassivePush);
    }

    protected override void StatReset()
    {
        stat.MaxHP = 14;
        stat.MaxSP = 2;
        stat.ATK = 4;
        stat.SATK = 4;
        stat.CriPer = 5;
        stat.CriDmg = 1.5f;
        stat.WEIGHT = 1;

        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        passiveCool = 5;
        currentPassiveCool = 0;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.CompareTag("Player"))
        {
            lastAr = collision.gameObject.GetComponent<Ar>();
        }
    }
    
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.transform.CompareTag("Player"))
        {
            Ar target = collision.gameObject.GetComponent<Ar>();
            StartCoroutine(SkillCameraMove(collision.transform, target));
        }
    }
    
    private IEnumerator SkillCameraMove(Transform targetTransform, Ar targetAr)
    {
        yield return new WaitForSeconds(1f);
        CameraMove.Instance.MovetoTarget(targetTransform);
        yield return new WaitForSeconds(0.5f);
        BattleManager.Instance.SettingAr(targetAr, this);
        CameraMove.Instance.Shake();
    }
    
    private void PassiveDP()
    {
        if(lastAr != null && currentPassiveCool == 0)
        {
            Debug.Log("방깍사용");
            currentPassiveCool = passiveCool;
            lastAr.DecreaseSP(1);
            lastAr = null;
        }
    }
    
    private void PassivePush()
    {
        if(stat.HP <= 3 && canUsePassive)
        {
            canUsePassive = false;
            StartCoroutine("PassivePushCo");
        }
    }
    
    private IEnumerator PassivePushCo()
    {
        yield return new WaitForSeconds(0.3f);
        CameraMove.Instance.MovetoTarget(this.transform);
        yield return new WaitForSeconds(0.5f);
        Vector3 dir;
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, 3f);
        Debug.Log("패시브 사용");
        transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        foreach (Collider2D col in cols)
        {
            if (col.CompareTag("Player"))
            {
                dir = Vector3.Normalize(col.transform.position - transform.position);
                col.GetComponent<Rigidbody2D>().velocity = dir * 10f;
            }
        }
        yield return new WaitForSeconds(0.5f);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
    }
}

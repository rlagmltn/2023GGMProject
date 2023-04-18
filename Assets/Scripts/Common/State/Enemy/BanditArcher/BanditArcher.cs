using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditArcher : Enemy
{
    private float atkRange;
    public float AtkRange => atkRange;

    [SerializeField] private GameObject arrowGameObject;
    
    protected override void Start()
    {
        base.Start();
    }
    
    public override void StatReset()
    {
        stat.MaxHP = 10;
        stat.MaxSP = 4;
        stat.ATK = 3;
        stat.SATK = 3;
        stat.CriPer = 5;
        stat.CriDmg = 1.5f;
        stat.WEIGHT = 4;
    
        minDragPower = 0.2f;
        maxDragPower = 1.5f;
        pushPower = 20;
        atkRange = 6f;
        base.StatReset();
    }

    public void ShootArrow(Vector2 angle, bool isLast, bool isSkill = false)
    {
        if(arrowGameObject == null)
        {
            Debug.LogWarning("arrowGameObject is Null");
            return;
        }
        float rangeAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(arrowGameObject, transform.position, Quaternion.Euler(new Vector3(0, 0, rangeAngle)));
        if(isSkill)
        {
            bullet.GetComponent<Bullet>().damage = stat.ATK - 2;
            //need deburf
        }
        if(isLast)
        {
            bullet.GetComponent<Bullet>().damage = stat.ATK * 2;
        }
        cameraMove.MovetoTarget(bullet.transform);
    }
}

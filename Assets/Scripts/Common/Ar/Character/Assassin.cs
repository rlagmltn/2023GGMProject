using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assassin : Player
{
    [SerializeField] Bullet kunai;
    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
        Passive();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        this.angle = angle;
        animationManager.Throw();
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        Shoot(angle);
    }

    protected override void Passive()
    {
        stat.CriDmg *= 2.5f;
    }

    void Shoot(Vector2 angle)
    {
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(kunai, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        cameraMove.MovetoTarget(bullet.transform);
        cameraMove.Shake();
    }
}

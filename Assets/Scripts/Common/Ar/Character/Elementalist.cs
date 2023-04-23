using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : Player
{
    [SerializeField] Bullet elemental;
    private Vector2 angle;

    protected override void Start()
    {
        base.Start();
    }

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }

    protected override void Skill(Vector2 angle)
    {
        this.angle = angle;
        AnimSkillStart();
    }

    protected override void Passive()
    {
        //세 턴이 지날 때 마다 치명타 확률이 2%씩 증가한다.
    }

    public override void AnimTimingSkill()
    {
        Shoot(angle);
    }

    void Shoot(Vector2 angle)
    {
        //전방 6칸 방향으로 정령을 보내 폭발 시키며, 주변 범위 2칸내에 있는 적에게 2.5칸 대미지를 준다.
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(elemental, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        cameraMove.MovetoTarget(bullet.transform);
        cameraMove.Shake();
    }
}

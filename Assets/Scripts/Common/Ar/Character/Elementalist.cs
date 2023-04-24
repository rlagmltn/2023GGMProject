using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : Player
{
    [SerializeField] Bullet elemental;
    private int turnCount;
    private Vector2 angle;

    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
    }

    protected override void Skill(Vector2 angle)
    {
        this.angle = angle;
        AnimSkillStart();
    }

    protected override void Update()
    {
        if (TurnManager.Instance.PassedTurn != turnCount)
            Passive();
        base.Update();
    }

    protected override void Passive()
    {
        turnCount = TurnManager.Instance.PassedTurn;
        if (turnCount % 3 == 0)
        {
            turnCount = 0;
            stat.CriPer += 2;
        }
        //�� ���� ���� �� ���� ġ��Ÿ Ȯ���� 2%�� �����Ѵ�.
    }

    public override void AnimTimingSkill()
    {
        Shoot(angle);
    }

    void Shoot(Vector2 angle)
    {
        //���� 6ĭ �������� ������ ���� ���� ��Ű��, �ֺ� ���� 2ĭ���� �ִ� ������ 2.5ĭ ������� �ش�.
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(elemental, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        cameraMove.MovetoTarget(bullet.transform);
        cameraMove.Shake();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elementalist : Player
{
    [SerializeField] Bullet elemental;
    private int turnCount;
    private Vector2 angle;
    private Transform skillCircle;
    private float range;

    float maxTime = 1;
    float currentTime = 0;
    bool timeGoing = false;


    public override void StatReset()
    {
        isRangeCharacter = false;
        base.StatReset();
        skillCircle = skillRange.transform.GetChild(0);
        range = elemental.bulletSO.speed * elemental.bulletSO.lifeTime;
    }

    protected override void Skill(Vector2 angle)
    {
        base.Skill(angle);
        animationManager.Cast();
        this.angle = angle;
        WaitSec(2f);
    }

    protected override void Update()
    {
        if (TurnManager.Instance.PassedTurn != turnCount)
            Passive();
        base.Update();

        if (timeGoing) currentTime += Time.deltaTime;
        if (maxTime <= currentTime)
        {
            TurnManager.Instance.SomeoneIsMoving = false;
            timeGoing = false;
            currentTime = 0;
        }
    }

    public override void Drag(float angle, float dis)
    {
        base.Drag(angle, dis);

        var target = Physics2D.RaycastAll(transform.position, attackRange.transform.position - transform.position, range);

        if (target.Length > 1 && target[1].collider.CompareTag("Enemy"))
        {
            var a = Vector2.Distance(transform.position, target[1].collider.transform.position);
            skillRange.size = new Vector2(a / 2, 1);
            skillCircle.localPosition = new Vector2(a / 2 - 0.5f, 0);
        }
        else
        {
            skillRange.size = new Vector2(range / 2, 1);
            skillCircle.localPosition = new Vector2(range / 2 - 0.5f, 0);
        }
    }

    protected override void Passive()
    {
        turnCount = TurnManager.Instance.PassedTurn;
        if (turnCount % 3 == 0)
        {
            turnCount = 0;
            stat.CriPer += 2;
        }
        //세 턴이 지날 때 마다 치명타 확률이 2%씩 증가한다.
    }

    public override IEnumerator AnimTimingSkill()
    {
        while (isSkill) yield return null;
        Shoot(angle);
    }

    void Shoot(Vector2 angle)
    {
        //전방 6칸 방향으로 정령을 보내 폭발 시키며, 주변 범위 2칸내에 있는 적에게 2.5칸 대미지를 준다.
        float zAngle = Mathf.Atan2(angle.y, angle.x) * Mathf.Rad2Deg;
        var bullet = Instantiate(elemental, transform.position, Quaternion.Euler(0, 0, zAngle - 180));
        cameraMove.MovetoTarget(bullet.transform);
    }

    void WaitSec(float sec)
    {
        TurnManager.Instance.SomeoneIsMoving = true;
        timeGoing = true;
        maxTime = sec;
        currentTime = 0;
    }
}

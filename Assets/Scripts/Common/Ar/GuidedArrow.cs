using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedArrow : Arrow
{
    private Player ThisCharacter;
    private float CharacterDistance;

    private void Start()
    {
        CharacterDistance = 100f;
        GetNearCharacter();
    }

    protected override void Update()
    {
        transform.Translate(Vector2.right * GetBullet().speed * Time.deltaTime);
    }

    protected override void SetSO()
    {
        summoner = ThisCharacter;
        base.SetSO();
    }

    void GetNearCharacter()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 10);

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Enemy>()) continue;

            if (Vector2.Distance(transform.position, hit[num].transform.position) > CharacterDistance) continue;
            CharacterDistance = Vector2.Distance(transform.position, hit[num].transform.position);
            ThisCharacter = hit[num].GetComponent<Player>();
        }
    }
}

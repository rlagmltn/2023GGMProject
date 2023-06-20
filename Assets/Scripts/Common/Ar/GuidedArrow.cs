using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedArrow : Arrow
{
    [SerializeField] private float CanDistance = 6;

    private Player ThisCharacter;
    private float CharacterDistance;

    private Vector2 StartPos;

    private void Start()
    {
        StartPos = transform.position;
        CharacterDistance = 100f;
        
    }

    protected override void Update()
    {
        if(Vector2.Distance(StartPos, transform.position) > CanDistance * 6f ) Destroy(gameObject);
        transform.Translate(Vector2.right * GetBullet().speed * Time.deltaTime);
    }

    protected override void SetSO()
    {
        base.SetSO();
        GetNearCharacter();
        summoner = ThisCharacter;
        SetDamage(1);
    }

    void GetNearCharacter()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 10);
        Collider2D col = null;

        if (hit.Length <= 0) return;

        for (int num = 0; num < hit.Length; num++)
        {
            if (!hit[num].GetComponent<Player>()) continue;

            if (Vector2.Distance(transform.position, hit[num].transform.position) > CharacterDistance) continue;
            CharacterDistance = Vector2.Distance(transform.position, hit[num].transform.position);
            col = hit[num];
        }
        ThisCharacter = col.GetComponent<Player>();
    }
}

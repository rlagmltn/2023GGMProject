using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellectActMarker : MonoBehaviour
{
    [SerializeField] Transform move;
    [SerializeField] Transform attack;
    [SerializeField] Transform skill;

    [SerializeField] Transform marker;

    private void Start()
    {
        False();
    }

    public void M2Move()
    {
        marker.gameObject.SetActive(true);
        marker.position = move.position;
    }

    public void M2Attack()
    {
        marker.gameObject.SetActive(true);
        marker.position = attack.position;
    }

    public void M2Skill()
    {
        marker.gameObject.SetActive(true);
        marker.position = skill.position;
    }

    public void False()
    {
        marker.gameObject.SetActive(false);
    }
}

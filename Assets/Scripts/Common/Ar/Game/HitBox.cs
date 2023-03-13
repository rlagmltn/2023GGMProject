using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField] int rangeX;
    [SerializeField] int rangeY;
    [SerializeField] bool InfinityX;
    [SerializeField] bool InfinityY;

    private GameObject hitbox;

    private void Start()
    {
        hitbox = (GameObject)Resources.Load("Prefabs/Game/RedHitBox");

        var box = Instantiate(hitbox, transform.position + new Vector3(0, (float)rangeY/2), Quaternion.identity);
        Vector2 size = new Vector2(rangeX, rangeY);
        box.transform.localScale = size;
    }
}

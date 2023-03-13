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
    public GameObject Hitbox { get; }

    private void Awake()
    {
        hitbox = (GameObject)Resources.Load("Prefabs/Game/RedHitBox");

        var box = Instantiate(hitbox, transform.GetChild(0));
        box.transform.position = transform.position + new Vector3((float)rangeX/2, 0);
        box.transform.rotation = Quaternion.Euler(0, 0, 0);
        Vector2 size = new Vector2(rangeX, rangeY);
        box.transform.localScale = size;
    }
}

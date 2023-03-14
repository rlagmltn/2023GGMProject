using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int rangeX;
    public int rangeY;
    [SerializeField] int startX;
    [SerializeField] int startY;

    private GameObject hitbox;
    public GameObject Hitbox { get; set; }

    private void Awake()
    {
        hitbox = (GameObject)Resources.Load("Prefabs/Game/RedHitBox");

        Hitbox = Instantiate(hitbox, transform.GetChild(0));
        Vector2 size = new Vector2(rangeX, rangeY);
        Hitbox.transform.position = transform.position + new Vector3(startX, startY);
        Hitbox.transform.rotation = Quaternion.Euler(0, 0, 0);
        Hitbox.transform.localScale = size;
    }
}

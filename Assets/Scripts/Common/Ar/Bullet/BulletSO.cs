using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamType : int
{
    Player,
    Enemy,
    Neutral, //Áß¸³
    None
}
public enum RangeType : int
{
    Melee,
    Range,
    None,
}
public enum BulletType : int
{
    Nomal,
    Penetrate,
    Explosion,
    None
}

[CreateAssetMenu(fileName = "New Bullet", menuName = "SO/Bullet")]
public class BulletSO : ScriptableObject
{
    public string Name;
    public TeamType teamType;
    public RangeType rangeType;
    public BulletType bulletType;
    public int damage;
    public float speed;
    public float range;

    public float lifeTime;
}

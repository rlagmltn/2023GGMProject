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
    public float damage;
    public float radius;
    public float speed;

    public float lifeTime;
}

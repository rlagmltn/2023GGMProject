using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyInfoSO", menuName = "SO/EnemyInfoSO")]
public class EnemyInfoSO : ScriptableObject
{
    public Sprite EnemyImage;
    public string EnemyName;
    public string Summary;
}

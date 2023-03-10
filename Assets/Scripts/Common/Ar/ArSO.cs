using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArSO", menuName = "SO/ArSO")]
public class ArSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    public Sprite Image => image;

    [SerializeField] private string nameString;
    public string Name => nameString;

    [SerializeField] private float baseAtk;
    public float BaseAtk => baseAtk;

    public float currentAtk;

    [SerializeField] private float baseHP;
    public float BaseHP => baseHP;

    public float currentHP;

    [SerializeField] private float baseWeight;
    public float BaseWeight => baseWeight;

    public float currentWeight;

    public bool isTake = false;
    public bool isUse = false;

    [Multiline(5)]
    public string Explanation;
}

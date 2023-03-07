using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turn : MonoBehaviour
{
    [SerializeField] Image turnImage;
    public bool active { get; private set; }

    private void Start()
    {
        EnableTurn();
    }

    public void EnableTurn()
    {
        turnImage.color = Color.green;
        active = true;
        Debug.Log("enable");
    }

    public void DisableTurn()
    {
        turnImage.color = Color.gray;
        active = false;
        Debug.Log("disable");
    }
}

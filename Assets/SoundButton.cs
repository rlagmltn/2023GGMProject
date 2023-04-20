using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    Transform ON;
    Transform OFF;
    bool isSoundON = true;
    void Awake()
    {
        ON = transform.Find("SoundON");
        OFF = transform.Find("SoundOFF");
    }

    void SetSound()
    {
        isSoundON = !isSoundON;
        ON.gameObject.SetActive(isSoundON);
        OFF.gameObject.SetActive(!isSoundON);
    }
}

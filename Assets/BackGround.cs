using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackGround : MonoBehaviour
{
    [SerializeField] private Transform _logo;
    [SerializeField] private Vector3 _logoScale = Vector2.one * 1.5f;

    [SerializeField] private Transform _message;
    [SerializeField] private Vector3 _messageScale = Vector2.one * 1.5f;
    int lr = -1;
    int LR
    {
        get
        {
            lr *= -1;
            return lr;
        }
    }



    WaitForSeconds changeT = new WaitForSeconds(0.33f);
    WaitForSeconds waitT = new WaitForSeconds(0.03f);

    Color32 basic = new Color32(200, 255, 255, 255);
    Color32[] ID = new Color32[3] { new Color32(5, 0, 0, 0), new Color32(0, 5, 0, 0), new Color32(0, 0, 5, 0) };
    SpriteRenderer background;

    int count = 2;
    bool isPM = false;
    float duration = 0.3f;

    private void Start()
    {
        background = GameObject.Find("Back").GetComponent<SpriteRenderer>();
        background.color = basic;

        StartCoroutine(Change());

        _logo.transform.DOScale(_logoScale, duration).SetLoops(-1, LoopType.Yoyo);
        _message.transform.DOScale(_messageScale, duration).SetLoops(-1, LoopType.Yoyo);

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            _logo.transform.DORotate(new Vector3(0f, 0f, LR * 80), duration * 4f, RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            yield return new WaitForSeconds(duration * 4f);
        }
    }

    IEnumerator Change()
    {
        while (true)
        {
            ChangeColor();
            yield return changeT;
        }
    }
    void ChangeColor()
    {
        if (count < 2) count++;
        else count = 0;

        if (isPM)
        {
            StartCoroutine(Increase());
        }
        else
        {
            StartCoroutine(Decrease());
        }
        isPM = !isPM;
    }

    IEnumerator Increase()
    {
        for (int i = 0; i < 11; i++)
        {
            background.color += (Color)ID[count];
            Debug.Log((Color32)background.color);
            yield return waitT;
        }
    }

    IEnumerator Decrease()
    {
        for (int i = 0; i < 11; i++)
        {
            background.color -= (Color)ID[count];
            Debug.Log((Color32)background.color);
            yield return waitT;
        }
    }
}

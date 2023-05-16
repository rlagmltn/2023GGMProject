using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Turn : MonoBehaviour
{
    [SerializeField] Image turnImage;
    public bool active { get; private set; }
    private Coroutine coru;
    private Sequence disableSequence;

    private void Start()
    {
        DisableTurn();
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0);
        transform.DOScale(1, 0.3f);
    }

    public void EnableTurn()
    {
        turnImage.DOColor(Color.green, 0.3f);
        active = true;
    }

    public void EnableEnemyTurn()
    {
        turnImage.DOColor(Color.red, 0.3f);
        active = true;
    }

    public void DisableTurn()
    {
        turnImage.color = Color.gray;
        active = false;
    }

    public void SetActiveTurnObj(bool value)
    {
        DisableTurn();
        if (!value) transform.DOScale(0, 0.3f).OnComplete(()=> { gameObject.SetActive(value); });
        else gameObject.SetActive(true);
    }

    public void Blink()
    {
        coru = StartCoroutine(BlinkCoru());
    }

    public void StopBlink()
    {
        Color color = turnImage.color;
        color.a = 1;
        turnImage.color = color;
        StopCoroutine(coru);
    }

    public IEnumerator BlinkCoru()
    {
        Color color = turnImage.color;
        bool plus = false;
        while(true)
        {
            if (color.a >= 1)
            {
                plus = false;
                yield return new WaitForSeconds(0.2f);
            }
            else if (color.a <= 0)
            {
                plus = true;
                yield return new WaitForSeconds(0.2f);
            }

            if(plus) color.a += Time.deltaTime*3f;
            else color.a -= Time.deltaTime*3f;

            turnImage.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
    }
}

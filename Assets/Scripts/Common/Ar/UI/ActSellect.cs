using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSellect : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Open()
    {
        if (animator == null)
            gameObject.SetActive(true);
        else
            animator.SetTrigger("Open");
    }

    public void Skip()
    {
        if (animator == null)
            gameObject.SetActive(true);
        else
            animator.SetTrigger("Skip");
    }

    public void Close()
    {
        if (animator == null)
            gameObject.SetActive(true);
        else
            animator.SetTrigger("Close");
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}

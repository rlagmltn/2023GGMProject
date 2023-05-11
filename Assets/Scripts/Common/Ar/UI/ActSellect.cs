using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSellect : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Open()
    {
        animator.SetTrigger("Open");
    }

    public void Skip()
    {
        animator.SetTrigger("Skip");
    }

    public void Close()
    {
        animator.SetTrigger("Close");
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}

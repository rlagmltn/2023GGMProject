using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActSellect : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject sellectTutoPanel;

    public void Open()
    {
        if (animator == null)
            gameObject.SetActive(true);
        else
        {
            if(!SaveManager.Instance.TutoData.sellectTuto)
                sellectTutoPanel.SetActive(true);
            animator.SetTrigger("Open");
        }
    }

    public void Skip()
    {
        if (animator == null)
        {

        }
        else
            animator.SetTrigger("Skip");
    }

    public void Close()
    {
        if (animator == null)
        {

        }
        else
            animator.SetTrigger("Close");
    }

    private void ActiveFalse()
    {
        gameObject.SetActive(false);
    }
}

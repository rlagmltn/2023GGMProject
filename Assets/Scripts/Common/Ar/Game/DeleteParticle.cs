using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteParticle : MonoBehaviour
{
    [SerializeField] float breakTime;

    private void OnEnable()
    {
        Invoke("Break", breakTime);
    }

    private void Break()
    {
        Destroy(transform.parent.gameObject);
    }
}

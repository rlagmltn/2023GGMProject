using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchFieldChaser : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] Transform chase;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        transform.position = chase.position;
        if (!playerController.IsBatchMode) gameObject.SetActive(false);
    }
}

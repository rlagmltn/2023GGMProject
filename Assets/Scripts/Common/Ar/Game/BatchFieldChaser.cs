using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatchFieldChaser : MonoBehaviour
{
    private PlayerController playerController;
    private CameraMove cameraMove;
    private BoxCollider2D box;
    [SerializeField] Transform chase;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        cameraMove = FindObjectOfType<CameraMove>();
        box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        transform.position = chase.position;
        ChangeSize(cameraMove.OrthographicSize);
        if (!playerController.IsBatchMode) Destroy(gameObject);
    }

    private void ChangeSize(float size)
    {
        box.size = new Vector2(3 + 3 * size, 0.5f + 0.5f * size);
    }
}

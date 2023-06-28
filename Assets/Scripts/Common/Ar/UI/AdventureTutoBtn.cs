using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdventureTutoBtn : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        if (SaveManager.Instance.TutoData.adventrueTuto)
        {
            Destroy(this);
            return;
        }
        GetComponent<Button>().onClick.AddListener(StartIt);
    }

    private void StartIt()
    {
        if (SaveManager.Instance.TutoData.adventrueTuto) return;
        panel.SetActive(true);
    }
}

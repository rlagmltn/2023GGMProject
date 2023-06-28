using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingTutoBtn : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        if (SaveManager.Instance.TutoData.trainingTuto)
        {
            Destroy(this);
            return;
        }
        GetComponent<Button>().onClick.AddListener(StartIt);
    }

    private void StartIt()
    {
        if (SaveManager.Instance.TutoData.trainingTuto) return;
        panel.SetActive(true);
    }
}

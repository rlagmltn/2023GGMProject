using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoMain_Test : MonoBehaviour
{
    [SerializeField] Button GotoMainButton;

    private void Start()
    {
        GotoMainButton.onClick.RemoveAllListeners();
        GotoMainButton.onClick.AddListener(GotoMainScene);
    }

    void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}

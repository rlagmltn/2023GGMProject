using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoMap_Test : MonoBehaviour
{
    [SerializeField] Button GoToMapButton;

    private void Start()
    {
        GoToMapButton.onClick.RemoveAllListeners();
        GoToMapButton.onClick.AddListener(GotoMapScene);
    }

    void GotoMapScene()
    {
        SceneManager.LoadScene("Stage1Map");
    }
}

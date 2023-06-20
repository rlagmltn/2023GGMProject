using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoMain_Test : MonoBehaviour
{
    [SerializeField] Button SettingButton;
    [SerializeField] Button GotoMainButton;
    [SerializeField] Button NogoMainButton;
    [SerializeField] Button CancelButton;

    [SerializeField] Transform SettingPannel;
    [SerializeField] Transform BackgroundPannel;

    private void Start()
    {
        GotoMainButton.onClick.RemoveAllListeners();
        GotoMainButton.onClick.AddListener(GotoMainScene);
        SettingButton.onClick.RemoveAllListeners();
        SettingButton.onClick.AddListener(SettingButtonClick);
        CancelButton.onClick.RemoveAllListeners();
        CancelButton.onClick.AddListener(SettingPannelSetactiveFalse);
        NogoMainButton.onClick.RemoveAllListeners();
        NogoMainButton.onClick.AddListener(SettingPannelSetactiveFalse);
    }

    void SettingButtonClick()
    {
        SettingPannel.gameObject.SetActive(true);
        BackgroundPannel.gameObject.SetActive(true);
    }

    void SettingPannelSetactiveFalse()
    {
        SettingPannel.gameObject.SetActive(false);
        BackgroundPannel.gameObject.SetActive(false);
    }

    void GotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
}
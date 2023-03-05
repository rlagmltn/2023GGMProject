using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum PanelKinds
{
    START = 0,
    INVENTORY,
    SHOP,
}

public class MGMainUI : MonoBehaviour
{
    [SerializeField] Button StartButton;
    [SerializeField] Button InventoryButton;
    [SerializeField] Button ShopButotn;

    public List<GameObject> Panels;

    private void Awake()
    {
        AddButtonEvent(StartButton, () => ActivePanel(PanelKinds.START));
        AddButtonEvent(InventoryButton, () => ActivePanel(PanelKinds.INVENTORY));
        AddButtonEvent(ShopButotn, () => ActivePanel(PanelKinds.SHOP));
    }

    private void Start()
    {
        ActivePanel(PanelKinds.START);
    }

    void AddButtonEvent(Button button, UnityAction action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }

    void ActivePanel(PanelKinds panel)
    {
        foreach(GameObject obj in Panels) obj.SetActive(false);

        Panels[(int)panel].SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTestSlot : MonoBehaviour
{
    private ArSO so;
    private Player player;
    private Button button;
    private Image image;

    public void SetSO(ArSO so)
    {
        this.so = so;
        button = GetComponent<Button>();
        button.onClick.AddListener(SummonPlayer);
        image = transform.GetChild(0).GetComponent<Image>();
        image.sprite = so.characterInfo.Image;
        player = Instantiate(so.ArData, null);
    }

    private void SummonPlayer()
    {
        player.gameObject.SetActive(true);
        MainTestModeManager.Instance.SummonPlayer(player);
    }
}

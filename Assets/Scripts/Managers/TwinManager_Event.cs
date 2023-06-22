using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class DelayAndPopup
{
    public float DelayTime;
    public Transform Popup;
}
public class TwinManager_Event : MonoSingleton<TwinManager_Event>
{
    [SerializeField] private List<DelayAndPopup> DAP;

    private void Start()
    {
        DOTween.Init();
    }

    internal void EventStart()
    {
        Hide();
        StartCoroutine(Show());
    }
    private IEnumerator Show()
    {
        for (int num = 0; num < DAP.Count; num++)
        {
            yield return new WaitForSeconds(DAP[num].DelayTime);
            Animation(num);
        }
        DAP.Clear();
    }

    private void Hide()
    {
        for(int num = 0; num < DAP.Count; num++)
        {
            DAP[num].Popup.localScale = Vector3.one * 0.1f;
            DAP[num].Popup.gameObject.SetActive(false);
        }
    }

    private void Animation(int num)
    {
        DAP[num].Popup.gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        seq.Append(DAP[num].Popup.DOScale(1.1f, 0.2f));
        seq.Append(DAP[num].Popup.DOScale(1f, 0.1f));
    }

    internal void SetDAP(float Delay, Transform PopUp)
    {
        DelayAndPopup dap = new DelayAndPopup();
        dap.DelayTime = Delay;
        dap.Popup = PopUp;

        DAP.Add(dap);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundAdder : MonoBehaviour
{
    [SerializeField] Button[] buttonsInScene;
    private void Start()
    {
        AudioClip clip = SoundManager.Instance.GetOrAddAudioClips("click/btn (18)", Sound.EFFECT);

        buttonsInScene = FindObjectsOfType<Button>(true);

        foreach(Button btn in buttonsInScene)
        {
            btn.onClick.AddListener(() => { SoundManager.Instance.Play(clip, Sound.EFFECT); });
        }
    }
}

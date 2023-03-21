using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoundScene : MonoBehaviour
{
    [SerializeField] Button nextBtn;
    [SerializeField] Button soundBtn;
    [SerializeField] Transform[] makePoint;
    [SerializeField] AudioClip[] click;
    [SerializeField] AudioClip[] bump;

    private int nextCount = 0;
    private void Start()
    {
        foreach(AudioClip audio in click)
        {
            var btn = Instantiate(soundBtn, makePoint[0]);
            var source = btn.GetComponent<AudioSource>();
            var text = btn.transform.GetComponentInChildren<TextMeshProUGUI>();

            source.clip = audio;
            text.SetText(audio.name);
            btn.onClick.AddListener(() => { source.Play(); });
        }

        foreach (AudioClip audio in bump)
        {
            var btn = Instantiate(soundBtn, makePoint[1]);
            var source = btn.GetComponent<AudioSource>();
            var text = btn.transform.GetComponentInChildren<TextMeshProUGUI>();

            source.clip = audio;
            text.SetText(audio.name);
            btn.onClick.AddListener(() => { source.Play(); });
        }

        foreach (Transform transform in makePoint)
        {
            transform.gameObject.SetActive(false);
        }
        makePoint[0].gameObject.SetActive(true);

        nextBtn.onClick.AddListener(MoveSound);
    }

    public void MoveSound()
    {
        makePoint[nextCount].gameObject.SetActive(false);

        nextCount++;
        if (nextCount >= makePoint.Length)
            nextCount = 0;

        makePoint[nextCount].gameObject.SetActive(true);
    }
}

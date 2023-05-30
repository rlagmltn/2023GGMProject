using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource[] audioSources = new AudioSource[(int)Sound.MAXCOUNT];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        Init();
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        GameObject root = GameObject.Find("SoundManager");
        DontDestroyOnLoad(root);

        string[] soundNames = System.Enum.GetNames(typeof(Sound));
        for (int i = 0; i < (int)Sound.MAXCOUNT; i++)
        {
            GameObject obj = new GameObject { name = soundNames[i] };
            audioSources[i] = obj.AddComponent<AudioSource>();
            obj.transform.parent = root.transform;
        }

        audioSources[(int)Sound.BGM].loop = true;
        audioSources[(int)Sound.EFFECT].loop = false;
    }

    public void Play(AudioClip clip, Sound sound, float pitch = 1.0f)
    {
        AudioSource audioSource = audioSources[(int)sound];
        audioSource.pitch = pitch;
        if (sound == Sound.BGM)
        {
            if (audioSource.isPlaying) audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(clip);  
        }
    }
    public AudioClip GetOrAddAudioClips(string path, Sound sound = Sound.EFFECT)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}"; 

        AudioClip audioClip = null;

        if (sound == Sound.BGM) 
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
    public void Clear()
    {
        foreach (var audio in audioSources)
        {
            audio.clip = null;
            audio.Stop();
        }
        audioClips.Clear();
    }

    public void LoadScene(eSceneName name)
    {
        Clear();
    }
}

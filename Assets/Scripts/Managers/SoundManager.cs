using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource[] audioSources = new AudioSource[(int)Sound.MAXCOUNT];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("SoundManager");
        DontDestroyOnLoad(root);

        string[] soundNames = System.Enum.GetNames(typeof(Sound));
        for (int i = 0; i < soundNames.Length; i++)
        {
            GameObject go = new GameObject { name = soundNames[i] };
            audioSources[i] = go.AddComponent<AudioSource>();
            go.transform.parent = root.transform;
        }

        audioSources[(int)Sound.BGM].loop = true;
        
    }
}

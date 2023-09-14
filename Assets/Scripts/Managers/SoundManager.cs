using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerGroup bgm;
    [SerializeField] AudioMixerGroup effect;
    [SerializeField] AudioMixerGroup ui;
    public AudioSource[] audioSources = new AudioSource[(int)Sound.MAXCOUNT];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    [SerializeField] Slider masterS;
    [SerializeField] Slider bgmS;
    [SerializeField] Slider effectS;
    [SerializeField] Slider uiS;

    [SerializeField] Transform Master_ON;
    [SerializeField] Transform Master_OFF;
    [SerializeField] Transform BGM_ON;
    [SerializeField] Transform BGM_OFF;
    [SerializeField] Transform Effect_ON;
    [SerializeField] Transform Effect_OFF;
    [SerializeField] Transform UI_ON;
    [SerializeField] Transform UI_OFF;

    public float MasterValue = 0;
    public float BGMValue = 0;
    public float EffectValue = 0;
    public float UIValue = 0;

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
        audioSources[(int)Sound.BGM].outputAudioMixerGroup = bgm;
        audioSources[(int)Sound.EFFECT].loop = false;
        audioSources[(int)Sound.EFFECT].outputAudioMixerGroup = effect;
        audioSources[(int)Sound.UI].loop = false;
        audioSources[(int)Sound.UI].outputAudioMixerGroup = ui;
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

    public void SetMasterVolume()
    {
        audioMixer.SetFloat("Master", Mathf.Log10(masterS.value) * 20);

        if (masterS.value < 0.01)
        {
            Master_ON.gameObject.SetActive(false);
            Master_OFF.gameObject.SetActive(true);
        }
        else
        {
            Master_ON.gameObject.SetActive(true);
            Master_OFF.gameObject.SetActive(false);
        }
    }

    public void MasterVolume_ButtonClick()
    {
        if(masterS.value > 0.01f)
        {
            MasterValue = masterS.value;
            masterS.value = 0;
        }
        else
        {
            masterS.value = MasterValue;
            MasterValue = 0;
        }
        SetMasterVolume();
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(bgmS.value) * 20);
        
        if (bgmS.value < 0.01f)
        {
            BGM_ON.gameObject.SetActive(false);
            BGM_OFF.gameObject.SetActive(true);
        }
        else
        {
            BGM_ON.gameObject.SetActive(true);
            BGM_OFF.gameObject.SetActive(false);
        }
    }

    public void BGMVolume_ButtonClick()
    {
        if (bgmS.value > 0.01)
        {
            BGMValue = bgmS.value;
            bgmS.value = 0;
        }
        else
        {
            bgmS.value = BGMValue;
            BGMValue = 0;
        }
        SetBGMVolume();
    }

    public void SetEffectVolume()
    {
        audioMixer.SetFloat("Effect", Mathf.Log10(effectS.value) * 20);

        if (effectS.value < 0.01f)
        {
            Effect_ON.gameObject.SetActive(false);
            Effect_OFF.gameObject.SetActive(true);
        }
        else
        {
            Effect_ON.gameObject.SetActive(true);
            Effect_OFF.gameObject.SetActive(false);
        }
    }

    public void EffectVolume_ButtonClick()
    {
        if (effectS.value > 0.01)
        {
            EffectValue = effectS.value;
            effectS.value = 0;
        }
        else
        {
            effectS.value = EffectValue;
            EffectValue = 0;
        }
        SetEffectVolume();
    }

    public void SetUIVolume()
    {
        audioMixer.SetFloat("UI", Mathf.Log10(uiS.value) * 20);

        if (uiS.value < 0.01f)
        {
            UI_ON.gameObject.SetActive(false);
            UI_OFF.gameObject.SetActive(true);
        }
        else
        {
            UI_ON.gameObject.SetActive(true);
            UI_OFF.gameObject.SetActive(false);
        }
    }

    public void UIVolume_ButtonClick()
    {
        if (uiS.value > 0.01)
        {
            UIValue = uiS.value;
            uiS.value = 0;
        }
        else
        {
            uiS.value = UIValue;
            UIValue = 0;
        }
        SetUIVolume();
    }
}

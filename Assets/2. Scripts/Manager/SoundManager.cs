using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Bgm,
    Bear,
    Effect,
    MaxCount
}
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    AudioSource[] _audioSources = new AudioSource[(int)Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        instance ??= this;
        Init();
    }
    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Sound)); // "Bgm", "Card", "Eat", "Bear", "Turn"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Sound.Bgm].loop = true; // bgm ������ ���� �ݺ� ���
        }
    }

    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ���� Dictionary ����
        _audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Sound type = Sound.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else if (type == Sound.Bear)
        {
            AudioSource audioSource = _audioSources[(int)Sound.Bear];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Sound.Effect];
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Sound type = Sound.Effect, float volume = 1.0f, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, volume, pitch);
    }

    AudioClip GetOrAddAudioClip(string path, Sound type = Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
            path = $"Sounds/{path}"; // Sound ���� �ȿ� ����� �� �ֵ���

        AudioClip audioClip = null;

        if (type == Sound.Bgm)
        {
            audioClip = Resources.Load<AudioClip>(path);
        }
        else
        {
            if (_audioClips.TryGetValue(path, out audioClip) == false)
            {
                audioClip = Resources.Load<AudioClip>(path);
                _audioClips.Add(path, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {path}");

        return audioClip;
    }
}
    //public static SoundManager instance;
    //public AudioSource audioSource;

    //public List<AudioClip> soundList;

    //[Header("Card")]
    //public AudioClip draw;
    //public AudioClip eat;
    //public AudioClip drink;
    //public AudioClip decomposition;
    //public AudioClip sort;
    //public AudioClip merge;
    //public AudioClip hold;
    //public AudioClip drop;
    //public AudioClip destroy;

    //[Header("Bear")]
    //public AudioClip appear;
    //public AudioClip dead;

    //[Header("Turn")]
    //public AudioClip next;
    //public AudioClip clock;

    //[Header("Click Sound")]
    //public AudioClip click;

    //[Header("BGM")]
    //public AudioClip inGameBgm;
    //public AudioClip bearBattleBgm;
    //public AudioClip storyBgm;

    //private void Awake()
    //{
    //    instance ??= this;
    //    Init();
    //}
    //void Init()
    //{
    //    GameObject bgmobject = new GameObject("audioSource");
    //    bgmobject.transform.parent = transform;
    //    audioSource = bgmobject.AddComponent<AudioSource>();
    //}
    //public void StartSound(int i)
    //{
    //    audioSource.PlayOneShot(soundList[i]);
    //}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;

    public List<AudioClip> soundList;

    [Header("Draw Sound")]
    public AudioClip ds;

    [Header("Eat Sound")]
    public AudioClip ds1;

    [Header("Next Turn Sound")]
    public AudioClip ds2;

    [Header("Decomposition Sound")]
    public AudioClip ds3;

    [Header("Drink Sound")]
    public AudioClip ds4;

    [Header("bear appears Sound")]
    public AudioClip ds5;

    [Header("Card Sort Sound")]
    public AudioClip ds6;

    [Header("clock Roll Sound ")]
    public AudioClip ds7;

    [Header("Merge Sound")]
    public AudioClip ds8;

    [Header("Card Hold Sound")]
    public AudioClip ds9;

    [Header("Card Drop Sound")]
    public AudioClip ds10;

    [Header("Click Sound")]
    public AudioClip ds11;

    [Header("스토리 진행중")]
    public AudioClip ds12;

    [Header("곰 출현중")]
    public AudioClip ds13;

    [Header("Bear kill Sound")]
    public AudioClip ds14;

    [Header("Card Distroy Sound")]
    public AudioClip ds15;

    [Header("BGM")]
    public AudioClip ds16;
    //private void Awake()
    //{
    //    instance = this;
    //    Init();
    //}

    //void Init()
    //{
    //    GameObject bgmobject = new GameObject("audioSource");
    //    bgmobject.transform.parent = transform;
    //    audioSource = bgmobject.AddComponent<AudioSource>();
    //}
    public void StartSound(int i)
    {
        audioSource.PlayOneShot(soundList[i]);
    }
}
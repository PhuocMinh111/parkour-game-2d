using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Sound
{
    Jump,
    Jump2,
    Coin,
    Die,
    BGM,
    UIClick

}
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static AudioManager instance;
    void Awake()
    {
        instance = this;
    }
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    Dictionary<Sound, int> SoundMapping = new Dictionary<Sound, int>()
    {
        {Sound.Jump,1},
        {Sound.Jump2,2},
        {Sound.Die,4},
        {Sound.Coin,0},
        {Sound.BGM,0},
        {Sound.UIClick,3}
    };

    private void Start()
    {
        PlayBGM(Sound.BGM);
        StopSfx();
    }
    public void PlaySfx(Sound sound)

    {
        int index = SoundMapping[sound];
        if (index < sfx.Length)
        {
            sfx[index].Play();
        }
    }
    public void StopSfx()
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            sfx[i].Stop();
        }
    }

    public void PlayBGM(Sound sound)

    {
        int index = SoundMapping[sound];
        if (index < bgm.Length)
        {
            bgm[index].Play();
        }
    }
    public void StopBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }

    public void UIClick() => PlaySfx(Sound.UIClick);

}

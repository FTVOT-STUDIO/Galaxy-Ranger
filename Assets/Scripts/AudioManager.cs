using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    public AudioClip[] SfxClips;
    public float SfxVolume;
    public int channeles;
    AudioSource[] SfxPlayer;
    int channelIndex;

    public enum Sfx { N_Attack, A_Attack, L12_Attack =3, EnemyDamage, EnemyDead =6,
        PlayerDamage, playerDead, WeaponGet, HealGet = 11, boomGet, InvitGet, itemGet,levelUp,
        enemyAttack, levelDown, barriar , GameStart, Load, bossBoom, ablitySFx, bossAttack, bossLoad, bossDamage, bossEvent, bossBarriar, ablityOn}

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        GameObject sfxObj = new GameObject("SfxPlayer");
        sfxObj.transform.parent = transform;
        SfxPlayer = new AudioSource[channeles];

        for (int i =0; i < SfxPlayer.Length; i++)
        {
            SfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            SfxPlayer[i].playOnAwake = false;
            SfxPlayer[i].volume = SfxVolume; 
        }
    }

    public void PlayBgm(bool isPlay)
    {
        if (isPlay)
        {
            bgmPlayer.Play(0);
        }
        else
            bgmPlayer.Stop();
    }

    public void playSfx(Sfx sfx)
    {
        for (int i = 0; i < SfxPlayer.Length; i++)
        {
            int loppIndex = (i + channelIndex) % SfxPlayer.Length;

            if (SfxPlayer[loppIndex].isPlaying)
                continue;

            channelIndex = loppIndex;
            SfxPlayer[loppIndex].clip = SfxClips[(int)sfx];
            SfxPlayer[loppIndex].Play();
            break;
        }
           
    }
}

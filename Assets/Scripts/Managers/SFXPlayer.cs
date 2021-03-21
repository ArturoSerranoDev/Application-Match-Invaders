// ----------------------------------------------------------------------------
// SFXPlayer.cs
//
// Author: Arturo Serrano
// Date: 21/02/21
//
// Brief: Persistent controller that handles SFX over the app
// ----------------------------------------------------------------------------
using UnityEngine;

public class SFXPlayer : UnitySingletonPersistent<SFXPlayer>
{
    public AudioSource musicSource;
    
    [Header("AudioClips")] 
    public AudioClip backgroundMusic;
    public AudioClip playerShoot;
    public AudioClip enemyDestroyed;
    public AudioClip hit;
    public AudioClip victoryTheme;
    public AudioClip endGameTheme;
    public AudioClip defeatTheme;
    public AudioClip popSound;
    
    public void PlaySFX(AudioClip clip,AudioSource source, float volume, float pitch)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        
        source.Play();
    }

}

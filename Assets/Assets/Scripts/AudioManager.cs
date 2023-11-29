using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMasterConfig audioMasterConfig; // Configuraci�n maestra del audio

    public AudioChannel sfxChannelConfig;
    public AudioChannel musicChannelConfig;

    // Referencias a los AudioSources
    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    // M�todo para ajustar el volumen de un canal de audio espec�fico
    public void SetVolume(AudioChannel channelConfig, float volume)
    {
        channelConfig.volume = Mathf.Clamp01(volume); // Asegura que el volumen est� entre 0 y 1
        ApplyConfig(); // Aplica la nueva configuraci�n
    }

    // M�todo para silenciar o activar un canal de audio
    public void MuteChannel(AudioChannel channelConfig, bool isMuted)
    {
        channelConfig.isMuted = isMuted;
        ApplyConfig(); // Aplica la nueva configuraci�n
    }

    // M�todo para aplicar la configuraci�n actual a las fuentes de audio
    private void ApplyConfig()
    {
        // Configura el volumen y el estado de silencio para cada fuente de audio
        sfxAudioSource.volume = audioMasterConfig.sfxChannelConfig.volume * (audioMasterConfig.sfxChannelConfig.isMuted ? 0 : 1);
        musicAudioSource.volume = audioMasterConfig.musicChannelConfig.volume * (audioMasterConfig.musicChannelConfig.isMuted ? 0 : 1);
    }

    // M�todos para reproducir diferentes sonidos y m�sica
    public void PlayBulletSound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }

    public void PlayPursuitSound(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
    public void PlayBackgroundMusic(AudioClip clip)
    {
        musicAudioSource.clip = clip;
        musicAudioSource.Play();
    }
}


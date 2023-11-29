using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMasterConfig audioMasterConfig; // Configuración maestra del audio

    public AudioChannel sfxChannelConfig;
    public AudioChannel musicChannelConfig;

    // Referencias a los AudioSources
    public AudioSource sfxAudioSource;
    public AudioSource musicAudioSource;

    // Método para ajustar el volumen de un canal de audio específico
    public void SetVolume(AudioChannel channelConfig, float volume)
    {
        channelConfig.volume = Mathf.Clamp01(volume); // Asegura que el volumen esté entre 0 y 1
        ApplyConfig(); // Aplica la nueva configuración
    }

    // Método para silenciar o activar un canal de audio
    public void MuteChannel(AudioChannel channelConfig, bool isMuted)
    {
        channelConfig.isMuted = isMuted;
        ApplyConfig(); // Aplica la nueva configuración
    }

    // Método para aplicar la configuración actual a las fuentes de audio
    private void ApplyConfig()
    {
        // Configura el volumen y el estado de silencio para cada fuente de audio
        sfxAudioSource.volume = audioMasterConfig.sfxChannelConfig.volume * (audioMasterConfig.sfxChannelConfig.isMuted ? 0 : 1);
        musicAudioSource.volume = audioMasterConfig.musicChannelConfig.volume * (audioMasterConfig.musicChannelConfig.isMuted ? 0 : 1);
    }

    // Métodos para reproducir diferentes sonidos y música
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


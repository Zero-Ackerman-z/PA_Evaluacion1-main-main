using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AudioChannel", menuName = "Audio/Audio Channel")]
public class AudioChannel : ScriptableObject
{
    public float volume = 1.0f; // Volumen inicial del canal de audio
    public bool isMuted = false; // Estado de silencio del canal
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioMasterConfig", menuName = "Audio/Audio Master Config")]
public class AudioMasterConfig : ScriptableObject
{
    public AudioChannel masterChannelConfig;
    public AudioChannel sfxChannelConfig;
    public AudioChannel musicChannelConfig;
}

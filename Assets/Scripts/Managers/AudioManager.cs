using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.VisualScripting.Member;

public static class AudioManager
{

    public static void PlayLoop(GameObject source, AudioClip audio, float distance = float.MaxValue, float volume = 1f, float pitch = 1f)
    {
        AudioSource audioSource = PrepareSource(source, audio, distance, volume, pitch);
        audioSource.loop = true;
        audioSource.Play();
    }

    public static void StopLoop(GameObject source)
    {
        StopLoop(source.GetComponent<AudioSource>());
    }
    public static void StopLoop(AudioSource source)
    {
        MonoBehaviour.Destroy(source);
    }

    public static void Play(GameObject source,AudioClip audio,float distance = float.MaxValue, float volume = 1f,float pitch = 1f)
    {
        AudioSource audioSource = PrepareSource(source, audio, distance,volume, pitch);
        audioSource.Play();

        MonoBehaviour.Destroy(audioSource, audio.length / audioSource.pitch);
    }

    public static void PlayReversed(GameObject source, AudioClip audio, float distance = float.MaxValue, float volume = 1f, float pitch = 1f)
    {
        AudioSource audioSource = PrepareSource(source,audio,distance,volume,pitch);
        audioSource.loop = true;
        audioSource.pitch = -audioSource.pitch;
        audioSource.Play();

        MonoBehaviour.Destroy(audioSource, audio.length / -audioSource.pitch);
    }

    public static float RandomPitch(float randomPitch = 0f)
    {
        return 1 + Random.Range(-randomPitch, randomPitch);
    }

    private static AudioSource PrepareSource(GameObject source, AudioClip audio, float distance, float volume, float pitch)
    {
        AudioSource audioSource = source.AddComponent<AudioSource>();
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.spatialBlend = 1f;
        audioSource.minDistance = 0;
        audioSource.maxDistance = distance;
        audioSource.volume = volume * PlayerPrefs.GetFloat("SoundVolume", 1f);
        audioSource.clip = audio;
        audioSource.pitch = pitch;

        return audioSource;
    }

    public static void Play(Vector3 position, AudioClip audio, float distance = float.MaxValue, float volume = 1f, float pitch = 1f)
    {
        GameObject source = new GameObject();
        source.transform.position = position;
        AudioSource audioSource = PrepareSource(source, audio, distance, volume, pitch);
        audioSource.Play();

        MonoBehaviour.Destroy(source, audio.length / audioSource.pitch);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioCue", menuName = "ScriptableObjects/AudioCueScriptableObject", order = 1)]
public class AudioCueScriptableObject : ScriptableObject
{
    public AudioClip[] clips;
    public float minVolume;
    public float maxVolume;
    public float minPitch;
    public float maxPitch;
    [Range(0,1)]
    public float spatialBlend;

    public GameObject Play(Vector3 position)
    {
        // choose random variables
        AudioClip clip = clips[Random.Range(0, clips.Length)];
        float volume = Random.Range(minVolume, maxVolume);
        float pitch = Random.Range(minPitch, maxPitch);

        // play that thang
        GameObject obj = new GameObject(); // create a new GameObject to play from
        obj.transform.position = position;
        AudioSource audioSource = obj.AddComponent<AudioSource>();
        audioSource.spatialBlend = spatialBlend;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(clip, volume);
        Destroy(obj, clip.length / pitch); // clean up created object after done playing
        return obj;
    }
}

using System;
using UnityEngine;

public class AudioVolume : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    
    private AudioSource _audioSource;
    private bool _done;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_done)
        {
            return;
        }
        
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        _audioSource.PlayOneShot(audioClip);
        _done = true;
    }
}

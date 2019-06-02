using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AleatorySounds : MonoBehaviour
{
    private readonly float TIMER = 150f;

    AudioSource audioSource;
    public AudioClip[] clips;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    

    void Start()
    {
        StartCoroutine(NewSound());
    }

    IEnumerator NewSound()
    {
        yield return new WaitForSeconds(TIMER);
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.Play();
        StartCoroutine(NewSound());
    }
}

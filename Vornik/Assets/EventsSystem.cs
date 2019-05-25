using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSystem : MonoBehaviour
{
    public enum EventType {Sound, Aparition1, Aparition2, EndPuzzle };
    public EventType eventType;

    bool active = false;

    //Sound
    public AudioSource audioSourcePlayer;
    public AudioClip audioClip;
    public float velocity;
    bool activeSound = false;

    //Variables necesarias para Sanja, aparicion 1
    public EventsSystem eventSystemSound;
    //Juguetes aparicion 2
    public Rigidbody[] toys;
    public VLB_Samples.Rotater[] rotater;
    public float max, min;

    private void Update()
    {
        if (activeSound && eventType == EventType.Sound)
        {
            audioSourcePlayer.volume = Mathf.Lerp(audioSourcePlayer.volume, 0.4f, Time.deltaTime * velocity);
        }
        if (!activeSound && active && eventType == EventType.Sound)
        {
            audioSourcePlayer.volume = Mathf.Lerp(audioSourcePlayer.volume, 0f, Time.deltaTime * velocity*10);
            if (audioSourcePlayer.volume <= 0.001f)
            {
                audioSourcePlayer.volume = 0;
                audioSourcePlayer.Stop();
                audioSourcePlayer.loop = false;
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !active)
        {
            EnterCollisionPlayer();
            active = true;
        }
    }

    void EnterCollisionPlayer()
    {
        switch (eventType)
        {
            case EventType.Sound:
                activeSound = true;
                audioSourcePlayer.clip = audioClip;
                audioSourcePlayer.Play();
                audioSourcePlayer.loop = true;
                break;
            case EventType.Aparition1:
                eventSystemSound.EndSound();
                break;
            case EventType.Aparition2:
                ActivateToys();
                break;
            case EventType.EndPuzzle:
                break;
           
        }
    }

    public void EndSound()
    {
        activeSound = false;
    }

    void ActivateToys()
    {
        for (int i = 0; i < toys.Length; i++)
        {
            rotater[i].enabled = true;
            Vector3 newPosition = new Vector3(toys[i].gameObject.transform.position.x, Random.Range(min, max), toys[i].gameObject.transform.position.z);
            toys[i].useGravity = false;
            toys[i].gameObject.transform.position = newPosition;
        }
    }

    public void DesactivateToys()
    {
        for (int i = 0; i < toys.Length; i++)
        {
            rotater[i].enabled = false;
            toys[i].useGravity = true;
        }
    }
  
}

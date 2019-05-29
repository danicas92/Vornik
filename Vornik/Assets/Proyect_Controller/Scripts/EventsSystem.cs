using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsSystem : MonoBehaviour
{
    public enum EventType {Sound, Aparition1, Aparition2, Lloro, EndPuzzle };
    public EventType eventType;

    bool active = false;

    //Sound
    public AudioSource audioSourcePlayer;
    public AudioClip audioClip;
    public float velocity;
    bool activeSound = false;

    //Variables necesarias para Sanja, aparicion 1
    public EventsSystem eventSystemSound;
    public SanjaBehaviur sanjaBehaviur;
    //Juguetes aparicion 2
    public Rigidbody[] toys;
    public VLB_Samples.Rotater[] rotater;
    public ComportamientoObjetos[] comportamientoObj;
    public float max, min;
    public GameObject eventSound, apagon, door;
    bool desactiveToys = false;
    //Lloro
    public GameObject SanjaCry;

    private void Update()
    {
        if (activeSound && (eventType == EventType.Sound || eventType == EventType.Aparition2))
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
        if (desactiveToys)
        {
            audioSourcePlayer.volume = Mathf.Lerp(audioSourcePlayer.volume, 0f, Time.deltaTime * velocity * 15); 
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
                sanjaBehaviur.Run();
                break;
            case EventType.Aparition2:
                StartCoroutine(ObjectFly());
                break;
            case EventType.Lloro:
                SanjaCry.SetActive(true);
                break;
            case EventType.EndPuzzle:
                break;
           
        }
    }

    IEnumerator ObjectFly()
    {
        float timer = apagon.GetComponent<AudioSource>().clip.length;
        apagon.SetActive(true);
        yield return new WaitForSeconds(timer+0.4f);
        SanjaCry.SetActive(false);
        activeSound = true;
        audioSourcePlayer.clip = audioClip;
        audioSourcePlayer.Play();
        audioSourcePlayer.loop = true;
        ActivateToys();
        apagon.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(timer);
        apagon.SetActive(false);
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
        for (int i = 0; i < toys.Length-4; i++)
        {
            comportamientoObj[i].enabled = false;
            rotater[i].enabled = false;
            toys[i].useGravity = true;
            toys[i].isKinematic = false;
        }
        desactiveToys = true;
        activeSound = false;
        Destroy(eventSound);
    }
  
}

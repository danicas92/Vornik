using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SanjaBehaviur : MonoBehaviour
{

    private AudioSource audioSource;
    public AudioClip[] footSteps;
    public EventsSystem eventSystem;
    private int flag=0;
    private bool isRun = false;
    public float velocity;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (isRun)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * velocity);
        }
    }

    public void Run()
    {
        //Soltar dibujo y animacion correr
        audioSource.loop = false;
        audioSource.clip = footSteps[0];
        isRun = true;
        audioSource.volume = 0.2f;
        StartCoroutine(RunAgain());
    }

    IEnumerator RunAgain()
    {
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length*2);

        if (flag < footSteps.Length - 1) flag++;
        else flag = 0;

        audioSource.clip = footSteps[flag];
        StartCoroutine(RunAgain());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndSanja"))
        {
            StopAllCoroutines();
            eventSystem.EndSound();
            Destroy(this.gameObject);
        }
    }
}

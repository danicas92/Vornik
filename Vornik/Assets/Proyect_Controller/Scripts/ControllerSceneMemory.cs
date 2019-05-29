using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerSceneMemory : MonoBehaviour
{
    public TextMeshProUGUI text;
    public AudioSource audioSource;

    
    void Start()
    {
        StartCoroutine(Interrogation());
    }

    IEnumerator Interrogation()
    {
        yield return new WaitForSeconds(180f);
        text.text = "Esa mesa tiene algo extraño";
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        text.text = "";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleControl : MonoBehaviour
{
    [Header("Entradas para la configuración de los subtitulos")]
    [Tooltip("Array de strings para modificar de forma automática")]
    public string[] texts;
    [Tooltip("Array de audios para la modificación de forma automática")]
    public AudioClip[] clips;

    public Dialogue[] dialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Dialogue
{
    public string dialogue;
    public AudioClip audioClip;
}
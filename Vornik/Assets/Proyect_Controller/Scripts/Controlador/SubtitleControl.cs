using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SubtitleControl : MonoBehaviour
{
    [SerializeField] private AnswerWithHead answerScript;
    [SerializeField] private GameObject[] UI;

    [Header("Entradas para la configuración de los subtitulos")]
    [Tooltip("Array de strings para modificar de forma automática")]
    public string[] texts;
    [Tooltip("Array de audios para la modificación de forma automática")]
    public AudioClip[] clips;
    public int interactive;
    public bool action = false;

    public TextMeshProUGUI text;
    public AudioSource audioSource;
    int flag;

    ControllerManager controller;

    private void Awake()
    {
        controller = GameObject.Find("Controlador").GetComponent<ControllerManager>();
    }

    void Start()
    {
        text.text = "";
        flag = 0;
    }



    public void Answer(bool answer)
    {
        flag += answer ? 0 : 1;
        foreach (var ui in UI)
            Destroy(ui);
        //Desactivar UI contestar
        StartCoroutine(nameof(ReadLineSound));
    }

    public void ReadLine()
    {
        if (flag == interactive)
        {
            answerScript.enabled = true;
            text.text = "";
            foreach (var ui in UI)
                ui.SetActive(true);
            //Activar UI para contestar
        }//Accionar interaccion
        else if (flag < texts.Length)
            StartCoroutine(ReadLineSound());
        else
        {
            controller.ActivateTransition();
            text.text = "";
        }
            

    }

    IEnumerator ReadLineSound()
    {
        text.text = texts[flag];
        audioSource.clip = clips[flag];
        audioSource.Play();
        yield return new WaitForSeconds(clips[flag].length);
        if (flag == interactive)
            flag += 2;
        else
            flag++;
        ReadLine();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    static readonly float TIMER = 2f;

    SubtitleControl subtitleControl;
    PostProcessingControl postProcessingControl;


    private void Awake()
    {
        subtitleControl = GetComponent<SubtitleControl>();
        postProcessingControl = GameObject.Find("PostEffectPrefiles").GetComponent<PostProcessingControl>();
        
    }

    void Start()
    {
        postProcessingControl.RestartValue();
        StartCoroutine(StartScene());
    }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(TIMER);
        postProcessingControl.ActivateBlack(false);
        yield return new WaitForSeconds(TIMER/2);
        postProcessingControl.FocusChange(1);
        yield return new WaitForSeconds(TIMER);
        subtitleControl.ReadLine();
        //Subtitulos
    }
    
}

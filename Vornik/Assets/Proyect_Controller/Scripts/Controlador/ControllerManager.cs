using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControllerManager : MonoBehaviour
{
    static readonly float TIMER = 2f;

    SubtitleControl subtitleControl;
    PostProcessingControl postProcessingControl;
    StateGame stateGame;

    private AsyncOperation asyncOperation;


    private void Awake()
    {
        subtitleControl = GetComponent<SubtitleControl>();
        postProcessingControl = GameObject.Find("PostEffectPrefiles").GetComponent<PostProcessingControl>();
        stateGame = GetComponent<StateGame>();
    }

    void Start()
    {

        postProcessingControl.RestartValue();
        StartCoroutine(StartScene());


        //Comprobar que esta escena no la hemos hecho y autoguardar si es el caso.
        DataManager.LoadData(stateGame);
        if (SceneManager.GetActiveScene().buildIndex > Convert.ToInt32(stateGame.GetStage()))
        {
            stateGame.SetStage(Convert.ToString(SceneManager.GetActiveScene().buildIndex));
            DataManager.AutoSaveData(stateGame);
        }
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

    public void ActivateTransition()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        postProcessingControl.ActivateWhite(false);
        yield return new WaitForSeconds(TIMER);
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

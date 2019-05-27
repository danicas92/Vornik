using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ControllerScene : MonoBehaviour
{

    public PostProcessingControl postProcessingControl;
    private AsyncOperation asyncOperation;

    void Start()
    {
        StartCoroutine(Transition());
    }

    IEnumerator Transition()
    {
        yield return new WaitForSeconds(7f);
        postProcessingControl.ActivateWhite(false);
        yield return new WaitForSeconds(4f);
        asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

    }
}

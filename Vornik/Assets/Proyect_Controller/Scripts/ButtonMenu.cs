using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ButtonMenu : MonoBehaviour
{
    public enum TypeButton { NuevaPartida, Continuar, Creditos, Salir};
    public TypeButton typeButton;

    public void ActivateButton()
    {
        AsyncOperation operation;
        switch (typeButton)
        {
            case TypeButton.NuevaPartida:
                 operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case TypeButton.Continuar:
                 operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);//leer del archivo
                break;
            case TypeButton.Creditos:
                operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);//Poner escena de créditos
                break;
            case TypeButton.Salir:
                Application.Quit();
                break;
        }
    }
}

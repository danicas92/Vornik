using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorRueda : MonoBehaviour
{
    [SerializeField] private ControladorCandado[] controladoresCandado;
    [SerializeField] private GameObject Matrioshka;
    [SerializeField] private GameObject Jarron;
    [SerializeField] private AudioSource audio;

    private int contador;

    public void Check()
    {
        audio.Play();
        foreach (var controlador in controladoresCandado)
        {
            if (controlador.GetCorrent()) contador++;
        }
        if (contador == 3)
        {
            transform.Rotate(new Vector3(0,0,-90));
            Matrioshka.tag = "Grab";
            Jarron.tag = "Grab";
            this.enabled = false;
        }
        contador = 0;
    }

    private void OnDisable()
    {
        foreach (var controlador in controladoresCandado)
        {
            controlador.enabled = false;
        }
    }
}

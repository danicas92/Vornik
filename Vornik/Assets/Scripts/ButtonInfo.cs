using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInfo : MonoBehaviour
{
    [SerializeField] private bool _isRight;
    [SerializeField] private ControladorCandado controladorCandado;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Index"))
        {
            controladorCandado.Rota(_isRight);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    [SerializeField] private Transform falsoFondo;

    private bool _active = false;
    private float _augmento = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!_active && other.CompareTag("Index"))
        {
            _active = true;
        }
    }

    private void Update()
    {
        if (!_active) return;

        if (_augmento < 0.4f)
        {
            falsoFondo.Translate(new Vector3(0, -0.001f, 0));
            _augmento += 0.001f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{

    [SerializeField] private Transform falsoFondo;

    private bool active = false;
    private float augmento = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (!active && other.CompareTag("Index"))
        {
            active = true;
        }
    }

    private void Update()
    {
        if (!active) return;

        if (augmento < 0.4f)
        {
            falsoFondo.Translate(new Vector3(0, -0.001f, 0));
            augmento += 0.001f;
        }
    }
}

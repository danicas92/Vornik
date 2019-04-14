using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoLupa : MonoBehaviour
{
    /// <summary>
    /// Referenciamos cámarapara usar su rotación, con esta hacemos el efecto direccional
    /// </summary>
    public GameObject cameraParent;


    // Start is called before the first frame update
    /// <summary>
    /// Si no hay cámara referenciamos la principal
    /// </summary>
    void Start()
    {
        if (cameraParent == null)
            cameraParent = Camera.main.gameObject;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.rotation = cameraParent.transform.rotation;
    }
}

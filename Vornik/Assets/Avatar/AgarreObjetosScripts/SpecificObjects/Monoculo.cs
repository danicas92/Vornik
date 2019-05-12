using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monoculo : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private ObjectGrabber objectGrabber;

    private void Awake()
    {
        objectGrabber = GetComponent<ObjectGrabber>();
    }


    private void Update()
    {

        if (objectGrabber.GetGrabbed() && !camera.enabled)
        {
            camera.enabled = true;
            return;
        }
        else if(!objectGrabber.GetGrabbed())
        {
            camera.enabled = false;
        }

    }
}

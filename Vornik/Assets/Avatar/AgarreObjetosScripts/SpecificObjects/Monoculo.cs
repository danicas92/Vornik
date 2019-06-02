using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monoculo : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private ObjectGrabber _objectGrabber;

    private void Awake()
    {
        _objectGrabber = GetComponent<ObjectGrabber>();
    }


    private void Update()
    {

        if (_objectGrabber.GetGrabbed() && !camera.enabled)
        {
            camera.enabled = true;
            return;
        }
        else if(!_objectGrabber.GetGrabbed())
        {
            camera.enabled = false;
        }

    }
}

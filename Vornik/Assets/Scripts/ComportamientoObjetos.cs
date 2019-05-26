using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoObjetos : MonoBehaviour
{
    private ObjectGrabber scriptObjectGrabber;
    private Rigidbody rb;
    private VLB_Samples.Rotater rotater;
    private bool grabbed;
    private bool _firstInteraction = false;


    private void Awake()
    {
        scriptObjectGrabber = GetComponent<ObjectGrabber>();
        rotater = GetComponent<VLB_Samples.Rotater>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_firstInteraction && rotater.enabled)
        {
            _firstInteraction = true;
        }

        grabbed = scriptObjectGrabber.GetGrabbed();
        if (grabbed)
        {
            rotater.enabled = false;
        }
        else if(_firstInteraction)
        {
            rotater.enabled = true;
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}

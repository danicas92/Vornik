using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoObjetos : MonoBehaviour
{
    private ObjectGrabber _scriptObjectGrabber;
    private Rigidbody _rb;
    private VLB_Samples.Rotater rotater;
    private bool _grabbed;
    private bool _firstInteraction = false;


    private void Awake()
    {
        _scriptObjectGrabber = GetComponent<ObjectGrabber>();
        rotater = GetComponent<VLB_Samples.Rotater>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!_firstInteraction && rotater.enabled)
        {
            _firstInteraction = true;
        }

        _grabbed = _scriptObjectGrabber.GetGrabbed();
        if (_grabbed)
        {
            rotater.enabled = false;
        }
        else if(_firstInteraction)
        {
            rotater.enabled = true;
            _rb.isKinematic = true;
            _rb.useGravity = false;
        }
    }
}

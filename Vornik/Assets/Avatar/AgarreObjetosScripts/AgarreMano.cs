using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class AgarreMano : MonoBehaviour
{
    [SerializeField] private string InputName;
    [SerializeField] private BasePoseProvider basePoseProvider;
    [SerializeField] private Transform pivot;
    [SerializeField] private ControllerCollidersGrab controllerCollidersGrab;

    private GameObject _currentObject;
    private Vector3 _lastPosition;


    void Update()
    {
        if (Input.GetAxis(InputName) <= 0.15f && _currentObject != null)
        {
            _currentObject.GetComponent<ObjectGrabber>().Throw(transform.position, _lastPosition, transform.rotation, pivot);
            _currentObject = null;
            controllerCollidersGrab.enabled = false;
        }
        _lastPosition = transform.position;
    }

    public void OnTriggerStay(Collider colliderObject)
    {
        if (colliderObject.CompareTag("Grab") && Input.GetAxis(InputName) >= 0.15f && _currentObject == null)
        {
            _currentObject = colliderObject.gameObject;
            _currentObject.GetComponent<ObjectGrabber>().Grab(basePoseProvider,pivot);
            _currentObject.GetComponent<Rigidbody>().useGravity = false;
            controllerCollidersGrab.enabled = true;
        }
    }
}

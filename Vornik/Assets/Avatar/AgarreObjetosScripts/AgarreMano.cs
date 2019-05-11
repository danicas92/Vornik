using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class AgarreMano : MonoBehaviour
{
    [SerializeField] private string InputName;
    [SerializeField] private Transform pivot;
    [SerializeField] private bool isRight;

    private GameObject _currentObject;
    private Vector3 _lastPosition;

    void Update()
    {
        if (_currentObject !=null && _currentObject.GetComponent<ObjectGrabber>() == null)
            _currentObject = null;

        if (Input.GetAxis(InputName) <= 0.15f && _currentObject != null )
        {
            _currentObject.GetComponent<ObjectGrabber>().Throw(transform.position, _lastPosition,true);
            _currentObject = null;
        }
        _lastPosition = transform.position;
    }

    public void OnTriggerStay(Collider colliderObject)
    {
        if (_currentObject != null || colliderObject.GetComponent<ObjectGrabber>() == null)
            return;

        if (colliderObject.CompareTag("Grab") && Input.GetAxis(InputName) >= 0.15f && _currentObject == null && !colliderObject.GetComponent<ObjectGrabber>().GetGrabbed())
        {
            _currentObject = colliderObject.gameObject;
            _currentObject.GetComponent<ObjectGrabber>().Grab(pivot, isRight);
            _currentObject.GetComponent<Rigidbody>().useGravity = false;
        }
    }
}

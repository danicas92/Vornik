using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] private Vector3 rotacionAgarreDer;
    [SerializeField] private Vector3 rotacionAgarreIzq;

    private bool _isGrabbed;
    private Quaternion _pivotRotationInic;
    private Vector3 _lastPosition;
    private GameObject handGrabbing;

    private float MultiplyVeloc = 1.25f;//Multiplicador de la velocidad de lanzamiento

    public void Update()
    {
        _lastPosition = transform.position;
    }

    public bool GetGrabbed() { return _isGrabbed; }

    public void Grab(Transform pivot, bool derecha)
    {
        handGrabbing = pivot.parent.gameObject;
        transform.parent = pivot;
        transform.SetPositionAndRotation(pivot.position,pivot.rotation);
        var rot = derecha ? rotacionAgarreDer : rotacionAgarreIzq;
        transform.Rotate(rot);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
        SetCollidersTrigger(true);
        _isGrabbed = true;
    }

    public void Throw(Vector3 hand, Vector3 handLastPosition, bool activeRigidBody)//Se usa la pos de la mano para dejar el objeto ahy, la ultima pos de la mano para calcular la vel y la rotación para dejarlo con la rotaión actual de la mano
    {
        _isGrabbed = false;
        handGrabbing.GetComponentInChildren<ControllerCollidersGrab>().ForcedThrow();
        handGrabbing = null;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (activeRigidBody)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
            Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
            rb.velocity = CurrentVelocity * MultiplyVeloc;
        }
        SetCollidersTrigger(false);
        transform.parent = null;
    }

    private void SetCollidersTrigger(bool set)
    {
        var colliders = GetComponents<Collider>();
        var colliderChildren = GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
            collider.isTrigger = set;

    }

}

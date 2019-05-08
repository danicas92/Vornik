using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] private Vector3 rotacionAgarre;
    [SerializeField] private Vector3 posicionAgarre;

    private Quaternion _pivotRotationInic;
    private Vector3 _lastPosition;
    
    private float MultiplyVeloc = 1.25f;//Multiplicador de la velocidad de lanzamiento

    public void Update()
    {
        _lastPosition = transform.position;
    }

    public void Grab(BasePoseProvider bpp, Transform pivot)
    {
        //TRACKER POSE DRIVER
        /*
        _pivotRotationInic = pivot.localRotation;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        TrackedPoseDriver tpd = gameObject.AddComponent<TrackedPoseDriver>();
        tpd.poseProviderComponent = bpp;*/

        //REPARENT
        transform.parent = pivot;
        transform.SetPositionAndRotation(pivot.position,pivot.rotation);
        transform.Rotate(rotacionAgarre);
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void Throw(Vector3 hand, Vector3 handLastPosition)//Se usa la pos de la mano para dejar el objeto ahy, la ultima pos de la mano para calcular la vel y la rotación para dejarlo con la rotaión actual de la mano
    {
        //TRACKER POSE DRIVER
        /*
        pivot.localRotation = _pivotRotationInic; 

        TrackedPoseDriver tpd = GetComponent<TrackedPoseDriver>();
        Destroy(tpd);

        Rigidbody rb = GetComponent<Rigidbody>();

        this.GetComponent<Collider>().enabled = true;
        transform.localPosition = hand;
        transform.localRotation = handRotation;
        Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
        rb.velocity = CurrentVelocity * MultiplyVeloc;
        rb.useGravity = true;*/

        //REPARENT
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
        Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
        rb.velocity = CurrentVelocity * MultiplyVeloc;
        transform.parent = null;
    }

}

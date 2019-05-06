using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;
using UnityEngine.SpatialTracking;

public class ObjectGrabber : MonoBehaviour
{
    [SerializeField] private Vector3 positionRelAgarre;
    [SerializeField] private Vector3 rotationRelAgarre;
    private Vector3 _lastPosition;
    
    private float MultiplyVeloc = 1.25f;//Multiplicador de la velocidad de lanzamiento

    public void Update()
    {
        _lastPosition = transform.position;
    }

    public void Grab(BasePoseProvider bpp, Transform pivot)
    {
        TrackedPoseDriver tpd = gameObject.AddComponent<TrackedPoseDriver>();
        tpd.poseProviderComponent = bpp;
    }

    public void Throw(Vector3 hand, Vector3 handLastPosition, Quaternion handRotation)//Se usa la pos de la mano para dejar el objeto ahy, la ultima pos de la mano para calcular la vel y la rotación para dejarlo con la rotaión actual de la mano
    {
        TrackedPoseDriver tpd = GetComponent<TrackedPoseDriver>();
        Destroy(tpd);

        Rigidbody rb = GetComponent<Rigidbody>();

        this.GetComponent<Collider>().enabled = true;
        transform.position = hand;
        transform.rotation = handRotation;
        Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
        rb.velocity = CurrentVelocity * MultiplyVeloc;
        rb.useGravity = true;
    }

}

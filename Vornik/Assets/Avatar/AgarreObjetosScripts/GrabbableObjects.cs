using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.Experimental.XR.Interaction;
using SimpleVR;

public class GrabbableObjects : MonoBehaviour {

    private Vector3 _initialPosition;
    private Vector3 _lastPosition;
    [SerializeField] private Vector3 grabRotation;
    private float MultiplyVeloc = 1.25f;//Multiplicador de la velocidad de lanzamiento
    private bool _isGrabbed;
    
    [SerializeField] private int KeyObject = 1;//0 = importante, 1 = objeto normal, 2 = linterna

    //Para linterna
    private bool DejaLinterna = false;
    [SerializeField] private Transform transformBolsillo;

    public Vector3 GetGrabRotation()
    {
        return grabRotation;
    }

    public int GetTypeObject()
    {
        return KeyObject;
    }

    public void Awake()
    {
        _isGrabbed = false;
        _initialPosition = transform.position;
    }
    public void FixedUpdate()
    {
        //OVRInput.FixedUpdate();//SIEMPRE
    }
    public void Update()
    {
        //OVRInput.Update();//SIEMPRE

        _lastPosition = transform.position;
    }

    public void SetGrabbed(bool grabbed)
    {
        _isGrabbed = grabbed;
    }

    public bool GetGrabbed()
    {
        return _isGrabbed;
    }

    public void Throw(Vector3 hand,Vector3 handLastPosition,Quaternion handRotation)//Se usa la pos de la mano para dejar el objeto ahy, la ultima pos de la mano para calcular la vel y la rotación para dejarlo con la rotaión actual de la mano
    {
        TrackedPoseDriver tpd = GetComponent<TrackedPoseDriver>();
        Destroy(tpd);
        Rigidbody rb = GetComponent<Rigidbody>();

        this.GetComponent<Collider>().enabled = true;

        if (KeyObject == 0)
        {
            transform.position = _initialPosition;
        }
        else if (KeyObject == 1)
        {
            transform.position = hand;
            transform.rotation = handRotation;
            Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
            rb.velocity = CurrentVelocity * MultiplyVeloc;
            rb.useGravity = true;
        }
        else if (KeyObject == 2)
        {
            if (DejaLinterna)
            {
                transform.position = transformBolsillo.position;
                transform.rotation = transformBolsillo.rotation;
                rb.isKinematic = true;
            }
            else
            {
                transform.position = hand;
                transform.rotation = handRotation;
                Vector3 CurrentVelocity = (hand - handLastPosition) / Time.deltaTime;
                rb.velocity = CurrentVelocity * MultiplyVeloc;
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }

        SetGrabbed(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bolsillo"))
        {
            DejaLinterna = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bolsillo"))
        {
            DejaLinterna = false;
        }
    }
}

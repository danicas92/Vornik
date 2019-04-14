using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;
using UnityEngine.Experimental.XR.Interaction;
using SimpleVR;

public class GrabbingHand : MonoBehaviour {

    //[SerializeField] private handMovement handGrabMovement;

    [SerializeField] private GameObject pivot;
    [SerializeField] private Light lanternLight;

    private Vector3 lastPosition;
    private GameObject currentObject;
    [SerializeField] private string InputName;
    [SerializeField] private BasePoseProvider bpp;
    private bool Grabbing;//Comprueba si esta cogiendo algo
	
	void Awake () {
        Grabbing = false;
	}

    private void FixedUpdate()
    {
        //OVRInput.FixedUpdate();
    }

    void Update () {

        //OVRInput.Update();
        
        //Si suelta un objeto
        if (Input.GetAxis(InputName) <= 0.15f && currentObject!=null)
        {
            currentObject.GetComponent<GrabbableObjects>().Throw(transform.position,lastPosition,transform.rotation);
            currentObject = null;
            Grabbing = false;
        }
        
        if (currentObject != null && currentObject.GetComponent<GrabbableObjects>().GetTypeObject() == 2 && Input.GetButtonDown("Oculus_CrossPlatform_Button4") )
        {
            lanternLight.enabled = !lanternLight.enabled;
        }

        lastPosition = transform.position;
	}
    
    public void OnTriggerStay(Collider colliderObject)
    {
        //Mira si se puede coger, si hay otro en la mano, si estamos pulsando y si el objeto esta cogido por otra mano
        if (colliderObject.CompareTag("Grab") && Input.GetAxis(InputName)>=0.15f && !Grabbing && !colliderObject.gameObject.GetComponent<GrabbableObjects>().GetGrabbed())
        {
            currentObject = colliderObject.gameObject;
            GrabbableObjects grabbableObjects = currentObject.GetComponent<GrabbableObjects>();
            GrabObject(currentObject);
            Grabbing = true;
            grabbableObjects.SetGrabbed(true);
            colliderObject.GetComponent<Rigidbody>().useGravity = false;

            //handGrabMovement.Grab();
        }
    }

    public void GrabObject(GameObject Object)
    {
        Object.transform.position = Vector3.zero;
        Object.transform.rotation = Quaternion.identity;
        pivot.transform.rotation = Quaternion.identity;

        GrabbableObjects go = Object.GetComponent<GrabbableObjects>();
        //pivot.transform.Rotate( go.GetGrabRotation());

        TrackedPoseDriver tpd = Object.AddComponent<TrackedPoseDriver>();
        tpd.UseRelativeTransform = true;
        tpd.poseProviderComponent = bpp;
    }
    
}



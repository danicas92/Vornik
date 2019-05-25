using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] private Transform pivote;
    [SerializeField] private GameObject light;

    private ObjectGrabber _objectGrabber;
    private bool active = false;
    private string _buttDer = "Oculus_CrossPlatform_Button2";
    private string _buttIzq = "Oculus_CrossPlatform_Button4";

    private void Awake()
    {
        _objectGrabber = GetComponent<ObjectGrabber>();
    }

    private void Update()
    {
        if (_objectGrabber.GetGrabbed())
        {
            var hand = _objectGrabber.GetHandGrabbing;
            if ((Input.GetButtonDown(_buttDer) && hand.GetComponent<AgarreMano>().GetMano()) || (Input.GetButtonDown(_buttIzq) && !hand.GetComponent<AgarreMano>().GetMano()))
            {
                active = !active;
                light.SetActive(active);
            }
            return;
        }
            

        if (transform.position != pivote.position)
        {
            transform.position = pivote.position;
            transform.rotation = Quaternion.Euler(90,0,0);
            active = false;
            light.SetActive(false);
        }
    }
}

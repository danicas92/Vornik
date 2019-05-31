using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] private Transform pivote;
    [SerializeField] private GameObject light;
    [SerializeField] private GameObject marker;

    private ObjectGrabber _objectGrabber;
    private bool _active = false;
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
            marker.SetActive (false);
            var hand = _objectGrabber.GetHandGrabbing;
            if ((Input.GetButtonDown(_buttDer) && hand.GetComponent<AgarreMano>().GetMano()) || (Input.GetButtonDown(_buttIzq) && !hand.GetComponent<AgarreMano>().GetMano()))
            {
                _active = !_active;
                light.SetActive(_active);
            }
            return;
        }
        marker.SetActive(true);


        if (transform.position != pivote.position)
        {
            transform.position = pivote.position;
            transform.rotation = Quaternion.Euler(90,0,0);
            _active = false;
            light.SetActive(false);
        }
    }
}

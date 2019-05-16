using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linterna : MonoBehaviour
{
    [SerializeField] private Transform pivote;
    [SerializeField] private GameObject light;

    private ObjectGrabber _objectGrabber;
    private bool active = false;

    private void Awake()
    {
        _objectGrabber = GetComponent<ObjectGrabber>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Oculus_CrossPlatform_Button2"))
        {
            active = !active;
            light.SetActive(active);
        }

        if (_objectGrabber.GetGrabbed())
            return; 

        if (transform.position != pivote.position)
        {
            transform.position = pivote.position;
            transform.rotation = Quaternion.Euler(90,0,0);
            active = false;
            light.SetActive(false);
        }
    }
}

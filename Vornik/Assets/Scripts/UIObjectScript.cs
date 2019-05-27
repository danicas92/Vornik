using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private ObjectGrabber objectGrabber;

    private void Awake()
    {
        objectGrabber = GetComponentInParent<ObjectGrabber>();
    }

    private void Update()
    {
        if (objectGrabber.GetGrabbed() )
        {
            Destroy(transform.gameObject);

        }
    }


}

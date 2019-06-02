using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject UI;

    private ObjectGrabber _objectGrabber;

    private void Awake()
    {
        _objectGrabber = GetComponentInParent<ObjectGrabber>();
    }

    private void Update()
    {
        if (_objectGrabber.GetGrabbed() )
        {
            Destroy(transform.gameObject);

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationScript : MonoBehaviour
{

    [SerializeField] private GameObject ObjectInf;

    private ObjectGrabber _og;

    private void Awake()
    {
        _og = GetComponent<ObjectGrabber>();
    }

    private void Update()
    {
        if (_og.GetGrabbed())
        {
            ObjectInf.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationScript : MonoBehaviour
{

    [SerializeField] private GameObject ObjectInf;

    private ObjectGrabber og;

    private void Awake()
    {
        og = GetComponent<ObjectGrabber>();
    }

    private void Update()
    {
        if (og.GetGrabbed())
        {
            ObjectInf.SetActive(true);
        }
    }
}

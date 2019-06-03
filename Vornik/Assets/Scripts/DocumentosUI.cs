using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentosUI : MonoBehaviour
{
    [SerializeField] private GameObject UI;


    private ObjectGrabber _objectGrabber;
    private bool _grabbed = false;

    private void Awake()
    {
        _objectGrabber = GetComponent<ObjectGrabber>();
    }
    
    void Update()
    {
        if (_objectGrabber.GetGrabbed() && !_grabbed)
        {
            _grabbed = true;
            UI.SetActive(true);
        }
        else if (_grabbed && !_objectGrabber.GetGrabbed())
        {
            _grabbed = false;
            UI.SetActive(false);
        }
    }
}

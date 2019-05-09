using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaLaberinto : MonoBehaviour
{
    [SerializeField] private ObjectGrabber laberintoObjectGrabber;
    [SerializeField] private GameObject tapa;

    private Rigidbody rbBola;
    private bool _haveRigidbody;
    private bool _open = false;
    private float rotacion = 0;

    private void Awake()
    {
        rbBola = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (laberintoObjectGrabber.GetGrabbed() && !_haveRigidbody)
        {
            StartCoroutine(nameof(AddRigidbody));
            _haveRigidbody = true;
        }
        else if(!laberintoObjectGrabber.GetGrabbed())
        {
            Destroy(GetComponent<Rigidbody>());
            _haveRigidbody = false;
        }

        if (_open && rotacion < 90)
        {
            tapa.transform.Rotate(-1, 0, 0);
            rotacion++;

        }
        else if (rotacion >= 90)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator AddRigidbody()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.AddComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            _open = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaLaberinto : MonoBehaviour
{
    [SerializeField] private ObjectGrabber laberintoObjectGrabber;
    [SerializeField] private GameObject tapa;
    [SerializeField] private GameObject taza;

    private Rigidbody _rbBola;
    private Vector3 _positionInitial;
    private bool _haveRigidbody;
    private bool _open = false;
    private float _rotacion = 0;

    private void Awake()
    {
        _rbBola = GetComponent<Rigidbody>();
        _positionInitial = transform.localPosition;
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

        if (_open && _rotacion < 90)
        {
            if (!taza.activeSelf)
            {
                taza.SetActive(true);
                taza.transform.parent = null;
            }
            tapa.transform.Rotate(-1, 0, 0);
            _rotacion++;

        }
        else if (_rotacion >= 90)
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Laberinto"))
        {
            if (transform.localPosition != _positionInitial) transform.localPosition = _positionInitial;
        }
    }
}

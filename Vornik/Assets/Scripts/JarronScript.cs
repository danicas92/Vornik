using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarronScript : MonoBehaviour
{
    [SerializeField] private GameObject jarronTrozos;

    private Rigidbody _rb;
    private ObjectGrabber _objectGrabber;
    private bool _destroyed = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _objectGrabber = GetComponent<ObjectGrabber>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_destroyed && !_objectGrabber.GetGrabbed() && GoodVelocity() > 0.3f)
        {
            _destroyed = true;
            Instantiate(jarronTrozos,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }

    private float GoodVelocity()
    {
        var aux = _rb.velocity.x + _rb.velocity.y + _rb.velocity.z;
        return aux;
    }
}

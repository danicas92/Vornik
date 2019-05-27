using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivaPared : MonoBehaviour
{
    [SerializeField] private Transform puerta;
    [SerializeField] private GameObject fotoZledic;

    private bool collide = false;

    private void OnTriggerEnter(Collider other)
    {
        var trozoTetera = other.GetComponentInChildren<TrozoTetera>();
        if (!collide && trozoTetera != null && trozoTetera.GetHijos()==3)
        {
            DisableGrabbing(other.gameObject);
            collide = true;
        }
    }

    private void DisableGrabbing(GameObject tetera)
    {
        tetera.GetComponent<ObjectGrabber>().Throw(Vector3.zero, Vector3.zero, false);
        tetera.transform.SetPositionAndRotation(transform.position,transform.rotation);
        Destroy(tetera.GetComponent<ObjectGrabber>());
        puerta.Rotate(0, 0, -82);
        fotoZledic.SetActive(true);
    }

}

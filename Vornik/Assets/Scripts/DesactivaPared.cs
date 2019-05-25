using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactivaPared : MonoBehaviour
{
    [SerializeField] private GameObject[] paredDesactivar;
    [SerializeField] private GameObject paredActivar;

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
        //tetera.transform.SetParent(transform);
        Destroy(tetera.GetComponent<ObjectGrabber>());
        ChangeWalls();
    }

    private void ChangeWalls()
    {
        foreach (var des in paredDesactivar)
            des.SetActive(false);
        //paredActivar.SetActive(true);
        this.enabled = false;
    }
}

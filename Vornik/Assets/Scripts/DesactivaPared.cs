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
        if (!collide && other.GetComponentInChildren<TrozoTetera>() != null)
        {
            DisableGrabbing(other.gameObject);
            collide = true;
        }
    }

    private void DisableGrabbing(GameObject tetera)
    {
        tetera.GetComponent<ObjectGrabber>().Throw(transform.position, transform.position, true);
        tetera.transform.SetParent(transform);
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

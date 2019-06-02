using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePictureWithCup : MonoBehaviour
{
    [SerializeField] private GameObject picture;
    [SerializeField] private string nameToCollide;

    private bool _colocado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == nameToCollide && !_colocado)
        {
            var objectGrabb = other.GetComponent<ObjectGrabber>();
            objectGrabb.Throw(Vector3.zero,Vector3.zero,false);
            objectGrabb.transform.SetPositionAndRotation(transform.position, transform.rotation);
            Destroy(objectGrabb);
            _colocado = true;
            ActivatePicture();
        }
    }

    private void ActivatePicture() { picture.SetActive(true); }

}

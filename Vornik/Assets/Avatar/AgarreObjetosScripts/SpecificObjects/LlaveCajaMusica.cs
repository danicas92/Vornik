using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LlaveCajaMusica : MonoBehaviour
{
    [SerializeField] private Transform pivoteInsert;
    [SerializeField] private Vector3 rotation = new Vector3(0, 0, 270);
    [SerializeField] private Transform tapa;

    private bool _rotate = false;
    private float _keyRotation = 0;
    private bool _open;
    private float _tapRotation = 0;

    private void Update()
    {
        if (_rotate && _keyRotation < 360)
        {
            transform.Rotate(0, 2, 0);
            _keyRotation += 2;
            _open = _keyRotation == 360? true : false;
        }

        if (_open && _tapRotation<90)
        {
            tapa.Rotate(-1,0,0);
            _tapRotation++;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MusicBox") && !_rotate)
        {
            GetComponent<ObjectGrabber>().Throw(this.transform.position,this.transform.position, false);
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = pivoteInsert;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(rotation);
            Destroy(GetComponent<ObjectGrabber>());
            _rotate = true;
        }
    }
}

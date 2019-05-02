using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFinger : MonoBehaviour {

    [SerializeField] private Transform indexAnt;
    [SerializeField] private string triggerButton;
    [SerializeField] private List<StopFinger> fingersAnt;

    private float multiplyer;
    private float multiplyerAnt = 0;
    private float _maxRotacion;
    private Vector3 rotInic;
    private bool collisionDetected;
    private bool otherCollided;

    [SerializeField] private bool _isThumb;
    private float _maxThumb = -0.4f;

    private void Awake()
    {
        rotInic = indexAnt.transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        multiplyer = Input.GetAxis(triggerButton);
        if (_isThumb)
        {
            if (multiplyer == 1 && !collisionDetected && !otherCollided && indexAnt.transform.localRotation.z>_maxThumb)//Avanza
            {
                indexAnt.Rotate(0, 0, -0.05f * 50);
            }
            //RETOCAR PORQUE AVECES SE ATASCA EL DEDO
            else if(multiplyer == 0)//Retrocede
            {
                if (indexAnt.transform.localRotation.z < rotInic.z && _maxRotacion >= indexAnt.transform.localRotation.z)
                {
                    indexAnt.Rotate(0, 0, 0.05f * 50);
                }
            }
        }
        else
        {
            if (multiplyer > multiplyerAnt)//Avanza
            {
                if (!collisionDetected && !otherCollided)
                {
                    indexAnt.Rotate(0, 0, (-multiplyer + multiplyerAnt) * 50);
                    //_maxRotacion += multiplyer - multiplyerAnt;
                }
            }
            else if (multiplyer < multiplyerAnt)//Retrocede
            {
                Debug.Log(indexAnt.transform.localRotation.z + ":"+ rotInic.normalized.z);
                if ((_maxRotacion == 0 || multiplyer < _maxRotacion) && indexAnt.transform.localRotation.z < rotInic.normalized.z)
                {
                    indexAnt.Rotate(0, 0, (-multiplyer + multiplyerAnt) * 50);
                }
            }
            else if (multiplyer == 0)
            {
                indexAnt.localRotation = Quaternion.Euler(rotInic);
            }
            multiplyerAnt = multiplyer;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            collisionDetected = true;
            _maxRotacion = indexAnt.transform.localRotation.z;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grab"))
        {
            collisionDetected = false;
            _maxRotacion = 0;
        }
    }

    private void StopLastFingers()
    {
        foreach (var finger in fingersAnt)
        {
            finger.StopThisFinger();
        }
    }

    private void PlayLastFingers()
    {
        foreach (var finger in fingersAnt)
        {
            finger.PlayThisFinger();
        }
    }

    public void PlayThisFinger()
    {
        otherCollided = false;
    }

    public void StopThisFinger()
    {
        otherCollided = true;
    }
}

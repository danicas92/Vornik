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

    private bool CheckThumb()
    {
        if (indexAnt.transform.localRotation.eulerAngles.z < rotInic.z + 0.1f && indexAnt.transform.localRotation.eulerAngles.z > rotInic.z - 0.1f)
            return false;
        return true;
    }

    private void Update()
    {
        multiplyer = Input.GetAxis(triggerButton);
        if (_isThumb)
        {
            if (multiplyer == 1 && !collisionDetected && !otherCollided && multiplyerAnt < multiplyer)//Avanza
            {
                indexAnt.Rotate(0, 0, -0.05f * 50);
                multiplyerAnt += 0.05f;
            }
            else if(multiplyer == 0 && multiplyerAnt> multiplyer)
            { 
                indexAnt.Rotate(0, 0, 0.05f * 50);
                multiplyerAnt -= 0.05f;
            }
        }
        else
        {
            if (multiplyer > multiplyerAnt && _maxRotacion == 0)//Avanza
            {
                if (!collisionDetected && !otherCollided)
                {
                    if (multiplyer - multiplyerAnt < 0.05f) return;
                    indexAnt.Rotate(0, 0, -0.05f * 50);
                    multiplyerAnt += 0.05f;
                }
            }
            else if (multiplyer < multiplyerAnt)//Retrocede
            {
                if ((_maxRotacion == 0 || multiplyer < _maxRotacion) /*&& indexAnt.transform.localRotation.z < rotInic.normalized.z*/)
                {
                    if (multiplyer - multiplyerAnt > -0.05f) return;
                    
                    indexAnt.Rotate(0, 0, 0.05f * 50);
                    multiplyerAnt -= 0.05f;
                }
            }
            else if (multiplyer == 0)
            {
                indexAnt.localRotation = Quaternion.Euler(rotInic);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollisionFingers"))
        {
            collisionDetected = true;
            _maxRotacion = indexAnt.transform.localRotation.z;
            StopLastFingers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollisionFingers"))
        {
            collisionDetected = false;
            _maxRotacion = 0;
            if (_isThumb) _maxThumb = -0.4f;
            PlayLastFingers();
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
        collisionDetected = false;
    }

    public void StopThisFinger()
    {
        otherCollided = true;
    }

    public void Reset()
    {
        multiplyerAnt = 0;
        _maxRotacion = 0;
        _maxThumb = -0.4f;
        collisionDetected = false;
    }

    public void ResetRotation()
    {
        indexAnt.localRotation = Quaternion.Euler(rotInic);
        Reset();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopFinger : MonoBehaviour {

    [SerializeField] private Transform indexAnt;
    [SerializeField] private string triggerButton;
    [SerializeField] private List<StopFinger> fingersAnt;
    [SerializeField] private bool _isThumb;

    private float _multiplyer;
    private float _multiplyerAnt = 0;
    private float _maxRotacion;
    private Vector3 _rotInic;
    private bool _collisionDetected;
    private bool _otherCollided;

    private float _maxThumb = -0.4f;

    private void Awake()
    {
        _rotInic = indexAnt.transform.localRotation.eulerAngles;
    }

    private bool CheckThumb()
    {
        if (indexAnt.transform.localRotation.eulerAngles.z < _rotInic.z + 0.1f && indexAnt.transform.localRotation.eulerAngles.z > _rotInic.z - 0.1f)
            return false;
        return true;
    }

    private void Update()
    {
        _multiplyer = Input.GetAxis(triggerButton);
        if (_isThumb)
        {
            if (_multiplyer == 1 && !_collisionDetected && !_otherCollided && _multiplyerAnt < _multiplyer)//Avanza
            {
                indexAnt.Rotate(0, 0, -0.05f * 50);
                _multiplyerAnt += 0.05f;
            }
            else if(_multiplyer == 0 && _multiplyerAnt> _multiplyer)
            { 
                indexAnt.Rotate(0, 0, 0.05f * 50);
                _multiplyerAnt -= 0.05f;
            }
        }
        else
        {
            if (_multiplyer > _multiplyerAnt && _maxRotacion == 0)//Avanza
            {
                if (!_collisionDetected && !_otherCollided)
                {
                    if (_multiplyer - _multiplyerAnt < 0.05f) return;
                    indexAnt.Rotate(0, 0, -0.05f * 50);
                    _multiplyerAnt += 0.05f;
                }
            }
            else if (_multiplyer < _multiplyerAnt)//Retrocede
            {
                if ((_maxRotacion == 0 || _multiplyer < _maxRotacion) /*&& indexAnt.transform.localRotation.z < rotInic.normalized.z*/)
                {
                    if (_multiplyer - _multiplyerAnt > -0.05f) return;
                    
                    indexAnt.Rotate(0, 0, 0.05f * 50);
                    _multiplyerAnt -= 0.05f;
                }
            }
            else if (_multiplyer == 0)
            {
                indexAnt.localRotation = Quaternion.Euler(_rotInic);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollisionFingers"))
        {
            _collisionDetected = true;
            _maxRotacion = indexAnt.transform.localRotation.z;
            StopLastFingers();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollisionFingers"))
        {
            _collisionDetected = false;
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
        _otherCollided = false;
        _collisionDetected = false;
    }

    public void StopThisFinger()
    {
        _otherCollided = true;
    }

    public void Reset()
    {
        _multiplyerAnt = 0;
        _maxRotacion = 0;
        _maxThumb = -0.4f;
        _collisionDetected = false;
    }

    public void ResetRotation()
    {
        indexAnt.localRotation = Quaternion.Euler(_rotInic);
        Reset();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrozoTetera : MonoBehaviour
{
    [SerializeField] private int identificador;

    private int hijos = 0;

    public int GetHijos() => hijos;
    public void SetHijos(int hijosSum) => hijos += hijosSum;
    public int GetIdentificador() { return identificador; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeapotPiece") && other.GetComponent<ColliderInfo>().GetColliderIdentificador() == identificador)
        {
            
            if (other.GetComponentInParent<TrozoTetera>().GetIdentificador() > identificador) return;

            if (!GetComponent<ObjectGrabber>().GetGrabbed() || !other.GetComponentInParent<ObjectGrabber>().GetGrabbed()) return;
            
            other.gameObject.GetComponentInParent<TrozoTetera>().SetHijos(hijos+1);
            GetComponent<ObjectGrabber>().Throw(transform.position,transform.position,false);
            Destroy(GetComponent<ObjectGrabber>());
            Destroy(GetComponent<Rigidbody>());
            transform.parent = other.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);

            RemoveCollidersHijos();
            RemoveCollsionWithFingers();
        }
        
    }

    private void RemoveCollidersHijos()
    {
        var colliders = GetComponentsInChildren<Collider>();
        foreach (var coll in colliders)
            Destroy(coll);
    }

    private void RemoveCollsionWithFingers()
    {
        var fingersCollision = GetComponentsInChildren<Transform>();
        foreach (var collisionGO in fingersCollision)
        {
            if (collisionGO.CompareTag("CollisionFingers"))
            {
                collisionGO.tag = "Untagged";
            }
        }
    }
}

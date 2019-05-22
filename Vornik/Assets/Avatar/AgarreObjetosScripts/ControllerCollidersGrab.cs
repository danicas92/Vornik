using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCollidersGrab : MonoBehaviour
{
    private Collider[] colliders;
    private StopFinger[] stopFingers;

    private void Awake()
    {
        colliders = GetComponentsInChildren<Collider>();
        stopFingers = GetComponentsInChildren<StopFinger>();
    }

    public void ForcedThrow()
    {
        foreach (var fingerScript in stopFingers)
            fingerScript.PlayThisFinger();
    }

    private void OnEnable()
    {
        foreach (var collider in colliders)
            collider.enabled = true;
    }

    private void OnDisable()
    {
        foreach (var collider in colliders)
            collider.enabled = false;
        foreach (var finger in stopFingers)
            finger.Reset();
    }

    public void ResetGrab()
    {
        foreach (var finger in stopFingers)
            finger.ResetRotation();
    }
}

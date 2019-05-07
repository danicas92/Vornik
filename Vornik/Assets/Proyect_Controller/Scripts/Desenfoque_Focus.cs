using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desenfoque_Focus : MonoBehaviour
{

    public PostProcessingControl postProcessingControl;

    public LayerMask layermask;
    public float distance;
    Camera camera;


    private void Awake()
    {
        camera = Camera.main;
        postProcessingControl = GameObject.Find("PostEffectPrefiles").GetComponent<PostProcessingControl>();
    }


    
    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, distance, layermask.value))
        {
            postProcessingControl.FocusChange(0);
        }
        else
            postProcessingControl.FocusChange(1);
        
    }
}

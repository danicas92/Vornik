using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;

public class DesenfoqueIluminación : MonoBehaviour
{
    static readonly float MAX = -2f;
    static readonly float MIN = 0f;
    static readonly float MIN_BLOOM = 2f;
    static readonly float MAX_BLOOM = 20f;

    /// <summary>
    /// Para un box collider con 2*2 el máximo es 1.5 y min 0.05
    /// </summary>


    [Header("Perfil de postprocesado principal para el desenfoque y distancia del rayo")]
    public PostProcessVolume postProcessingProfile;
    public float distance,velocity;
    public LayerMask layermask;

    float postExposure = MIN;
    bool bloomActive = false;
    ColorGrading colorGrading;
    Bloom bloom;
    Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Start()
    {   //Reseteando el perfil de postprocesado al iniciar
        postProcessingProfile.profile.TryGetSettings(out bloom);
        postProcessingProfile.profile.TryGetSettings(out colorGrading);
        colorGrading.postExposure.value = MIN;
        bloom.intensity.value =  MIN_BLOOM;
    }

    
    void Update()
    {
        Debug.DrawRay(camera.transform.position, camera.transform.forward, Color.blue);
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, distance, layermask.value))
        {
            float distanceHit = Vector3.Distance(raycastHit.point, raycastHit.transform.position);
            float distancePosition = Vector3.Distance(this.transform.position, raycastHit.transform.position);
            postExposure = CalculeColorGrading(distanceHit, distancePosition);
            bloomActive = true;
        }
        else
        {
            postExposure = MIN;
            bloomActive = false;
        }
            

        colorGrading.postExposure.value = Mathf.Lerp(colorGrading.postExposure.value, postExposure, velocity * Time.deltaTime);
        if(bloomActive)
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, MAX_BLOOM, velocity * Time.deltaTime);
        else
            bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, MIN_BLOOM, velocity * Time.deltaTime);
    }

    float CalculeColorGrading(float distanceHit, float distancePosition)
    {
        if (distanceHit > 2.5f)
            return MIN;
        float porDistance =  (distancePosition * 100f) / 20f;
        float porDistanceMAX = 1/(100f / (-MAX * porDistance));

        return MAX + distanceHit + porDistanceMAX;
    }
}

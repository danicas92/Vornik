using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariable : MonoBehaviour
{
    private readonly float MAX=3f;
    private readonly float MIN=0.85f;
    private readonly float TIMER=0.5f;
    private float random;

    Light light;

    private void Awake()
    {
        light = GetComponent<Light>();    
    }

    private void Start()
    {
        //random = Random.Range(MIN, MAX);
        //StartCoroutine(ChangeRandom());
        
    }

    void Update()
    {
        light.intensity = Mathf.Lerp(light.intensity, Random.Range(MIN, MAX), Time.deltaTime*5);
    }

    IEnumerator ChangeRandom()
    {
        yield return new WaitForSeconds(TIMER);
        random = Random.Range(MIN, MAX);
        StartCoroutine(ChangeRandom());
    }
}

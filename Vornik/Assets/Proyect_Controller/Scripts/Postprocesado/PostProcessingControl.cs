using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingControl : MonoBehaviour
{
    static readonly float MAX = 8f;
    static readonly float DIF = 0.5f;

    [Header("Entradas para la configuración de los perfiles de postprocesado")]
    [Tooltip("Array de perfiles de postprocesado, el orden es: 0-> Salud, 1-> Blanco, 2-> Negro, 3-> Monocular")]
    public GameObject[] postProcessingProfiles;
    [Tooltip("Velocidad de transición entre los perfiles")]
    public float velocity;
    [Space]
    [Header("Activadores")]
    public int profileBlack,profileWhite;//1->Activado,0->Desactivado,>1->otra configuracion
    public bool activateBlack, activateWhite;

    //Efectos salud
    DepthOfField depthOfField;
    static readonly float DEPTHMIN = 0.1f, DEPTHMAX = 1f, TIMER = 1f;
    public int stateDepthOfField = 0; //0->Min, 1->Max, >1->Random


    private void Start()
    {
        activateBlack = true;
        activateWhite = false;
        postProcessingProfiles[1].SetActive(activateWhite);
    }

    void FixedUpdate()
    {

        //Transiciones
        if (activateBlack)
        {
            Vector3 vectorBlack;
            if (profileBlack == 1)
                vectorBlack = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[2].transform.position.y, transform.parent.position.y, Time.deltaTime * velocity), transform.parent.position.z);
            else
            {
                vectorBlack = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[2].transform.position.y, MAX, Time.deltaTime * velocity), transform.parent.position.z);
                if (MAX - vectorBlack.y < 1)
                {
                    activateBlack = false;
                    postProcessingProfiles[2].SetActive(activateBlack);
                }
                    
            }
            postProcessingProfiles[2].transform.position = vectorBlack;
        }

        if (activateWhite)
        {
            Vector3 vectorWhite;
            if (profileWhite == 1)
                vectorWhite = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[1].transform.position.y, transform.parent.position.y, Time.deltaTime * velocity), transform.parent.position.z);
            else
            {
                vectorWhite = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[1].transform.position.y, MAX, Time.deltaTime * velocity), transform.parent.position.z);
                if (MAX - vectorWhite.y < 1)
                {
                    activateWhite = false;
                    postProcessingProfiles[1].SetActive(activateWhite);
                }
                    
            }
            postProcessingProfiles[1].transform.position = vectorWhite;
        }

        //Salud
        if (stateDepthOfField == 0)
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, DEPTHMIN, velocity*2 * Time.deltaTime);
        else if (stateDepthOfField == 1)
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, DEPTHMAX, velocity*2 * Time.deltaTime);
        else
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, Random.Range(DEPTHMIN,DEPTHMAX/1.5f) , velocity * Time.deltaTime);

    }

    public void RestartValue()
    {
        profileBlack = 1;
        profileWhite = 0;
        postProcessingProfiles[2].transform.position = new Vector3(transform.parent.position.x, transform.parent.position.y, transform.parent.position.z);
        postProcessingProfiles[1].transform.position = new Vector3(transform.parent.position.x, MAX, transform.parent.position.z);

        //Salud
        postProcessingProfiles[0].GetComponent<PostProcessVolume>().profile.TryGetSettings(out depthOfField);
        depthOfField.focusDistance.value = DEPTHMIN;
    }

    public void ActivateBlack(bool state)
    {
        if (state)
        {
            profileBlack = 1;
            activateBlack = true;
        }
        else
        {
            profileBlack = 0;
        }
            
    }

    public void ActivateWhite(bool state)
    {
        if (state)
        {
            profileWhite = 0;
        }
        else
        {
            profileWhite = 1;
            activateWhite = true;
            postProcessingProfiles[1].SetActive(activateWhite);
        }       
    }

    public void FocusChange(int state)
    {
        if (state > 1)
            StartCoroutine(RandomFocus());

        stateDepthOfField = state;
    }

    IEnumerator RandomFocus()
    {
        yield return new WaitForSeconds(TIMER);
        stateDepthOfField = 1;
    }
}

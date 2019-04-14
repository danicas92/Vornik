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
    [Tooltip("Array de perfiles de postprocesado, el orden es: 1-> base, 2-> blanco")]
    public GameObject[] postProcessingProfiles;
    [Tooltip("Velocidad de transición entre los perfiles")]
    public float velocity;
    [Space]
    [Header("Activadores")]
    [Tooltip("Array de booleanos, activan el perfil de postprocesado correspondiente, el mismo orden del array de postprocesado")]
    public bool[] activation;

    private int profileAct,profileNew;
    private bool changing;



    // Start is called before the first frame update
    void Start()
    {
        profileAct = 0;
        profileNew = 0;
        changing = false;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < activation.Length; i++)
        {
            if (activation[i])
            {
                DefaultActivation();
                ChangeProfile(i);
            }
        }

        if (changing)
        {
            Vector3 vectorAct = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[profileAct].transform.position.y, MAX, Time.deltaTime * velocity), transform.parent.position.z);
            Vector3 vectorNew = new Vector3(transform.parent.position.x, Mathf.Lerp(postProcessingProfiles[profileNew].transform.position.y, 0f, Time.deltaTime * velocity), transform.parent.position.z);

            postProcessingProfiles[profileAct].transform.position = vectorAct;
            postProcessingProfiles[profileNew].transform.position = vectorNew;

            if (Mathf.Abs(MAX-postProcessingProfiles[profileAct].transform.position.y)< DIF && postProcessingProfiles[profileNew].transform.position.y <= 0.5f)
            {
                Debug.Log("Terminado intercambio");
                changing = false;
                profileAct = profileNew;
            }
        }
    }

    void DefaultActivation()
    {
        for (int i = 0; i < activation.Length; i++)
        {
            activation[i] = false;
        }
    }

    void ChangeProfile(int newProfile)
    {
        if (!changing)
        {
            profileNew = newProfile;
            if (profileAct != profileNew)
                changing = true;
        }
    }
}

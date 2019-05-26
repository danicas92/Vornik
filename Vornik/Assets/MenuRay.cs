using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuRay : MonoBehaviour
{
    public LayerMask layermask;
    public TextMeshProUGUI text;
    bool actived = false;
    private string buttDer = "Oculus_CrossPlatform_Button2";

    void FixedUpdate()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit rayCastHit;

        if (Physics.Raycast(ray, out rayCastHit, Mathf.Infinity, layermask.value))
        {
            text = rayCastHit.collider.gameObject.GetComponent<TextMeshProUGUI>();
            if (!actived)
            {
                text.gameObject.GetComponent<AudioSource>().Play();
                text.fontStyle = FontStyles.Bold;
            }
            actived = true;
        }
        else
        {
            if(actived)
                text.fontStyle = FontStyles.Normal;
            actived = false;
        }
            

        if (actived && Input.GetButtonDown(buttDer))
        {
            text.gameObject.GetComponent<ButtonMenu>().ActivateButton();
        }
        else
            return;



    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeleport : MonoBehaviour {

    public TeleportMovement teleport;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Oculus_CrossPlatform_Button2"))
        {
            teleport.SetDisplay(true);
        }
        else if(Input.GetButtonUp("Oculus_CrossPlatform_Button2"))
        {
            teleport.SetDisplay(false);
            teleport.Teleport();
        }
	}
}

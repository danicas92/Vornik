using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTeleport : MonoBehaviour {

    public TeleportMovement teleport;
	
	void Update () {
        if (Input.GetButtonDown("Oculus_CrossPlatform_Button1"))
        {
            teleport.SetDisplay(true);
        }
        else if(Input.GetButtonUp("Oculus_CrossPlatform_Button1"))
        {
            teleport.SetDisplay(false);
            teleport.Teleport();
        }
	}
}

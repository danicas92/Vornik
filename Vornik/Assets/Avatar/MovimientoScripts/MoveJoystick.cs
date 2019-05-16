using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJoystick : MonoBehaviour {

    //ROTACION
    [SerializeField] private bool puedeCambiar = true; //Si true es que no esta pulsado, si false es que esta pulsado
    [SerializeField] private float angleRotation;
    [SerializeField] private string rotationAxis;

    //MOVIMIENTO

    [SerializeField] private string movementAxisHorizontal;
    [SerializeField] private string movementAxisVertical;

    private void Awake()
    {
        rotationAxis = "Oculus_CrossPlatform_SecondaryThumbstickHorizontal";
        movementAxisHorizontal = "Oculus_CrossPlatform_PrimaryThumbstickHorizontal";
        movementAxisVertical = "Oculus_CrossPlatform_PrimaryThumbstickVertical";
    }

	// Update is called once per frame
	void Update ()
    {
        //ROTACION

        if (Input.GetAxis(rotationAxis) > 0.7f && puedeCambiar)
        {
            puedeCambiar = false;
            Vector3 aux = new Vector3(transform.rotation.x, transform.rotation.y + angleRotation, transform.rotation.z);
            transform.Rotate(aux);
        }
        else if (Input.GetAxis(rotationAxis) < -0.7f && puedeCambiar)
        {
            puedeCambiar = false;
            Vector3 aux = new Vector3(transform.rotation.x, transform.rotation.y - angleRotation, transform.rotation.z);
            transform.Rotate(aux);
        }
        else if(Input.GetAxis(rotationAxis) < 0.1f && Input.GetAxis(rotationAxis) > -0.1f)
        {
            puedeCambiar = true;
        }

        //MOVIMIENTO

        
        var posX = Input.GetAxis(movementAxisHorizontal);
        var posZ = Input.GetAxis(movementAxisVertical);
        /*
        var newPos = Camera.main.transform.right * Time.deltaTime * posX + Camera.main.transform.forward * Time.deltaTime * posZ;
        transform.Translate(-newPos);*/

        transform.Translate(posX*Time.deltaTime*1.5f,0,posZ*Time.deltaTime*1.5f);
        

        

	}
}

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
        rotationAxis = "Oculus_CrossPlatform_PrimaryThumbstickHorizontal";
        movementAxisHorizontal = "Oculus_CrossPlatform_SecondaryThumbstickHorizontal";
        movementAxisVertical = "Oculus_CrossPlatform_SecondaryThumbstickVertical";
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
        transform.Translate(Input.GetAxis(movementAxisHorizontal)*0.03f,0,Input.GetAxis(movementAxisVertical)*0.03f);
        

	}
}

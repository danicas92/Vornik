using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour {

    public GameObject positionMarker; //Posicion de la señal del suelo
    public Transform BodyTransforma; // Objeto a mover
    public LayerMask collisionLayers; //Layers correctos
    public LayerMask WallLayer;
    public float MaxAngle = 45f; //Maximo angulo del arco
    public float strenght = 10f; //Fuerza de salida del arco

    public bool collisionWithWall = false;

    private int maxVertex= 100;

    private float vertexDelta = 0.08f; //??
    [SerializeField] private LineRenderer arcRnderer;
    private Vector3 velocity;
    private Vector3 groundPos;
    private Vector3 lastNormal;
    private bool collisionWithGround = false;
    private List<Vector3> vertexArcList = new List<Vector3>();
    private bool display = false;



    public void Awake()
    {
        arcRnderer = GetComponent<LineRenderer>();
        arcRnderer.enabled=false;
        positionMarker.SetActive(false);
    }

    public void SetDisplay(bool active)//Activar y desactivar el renderizado del arco
    {
        arcRnderer.enabled = active;
        positionMarker.SetActive(active);
        display = active;
    }

    public void Teleport()//Transporta el cuerpo a el sitio marcado
    {
        if (collisionWithGround && !collisionWithWall)
        {
            BodyTransforma.position = groundPos + lastNormal ;
        }
    }

    private void FixedUpdate() //Si se muestra el renderizado del arco lo actualiza
    {
        if (display)
        {
            UpdatePath();
        }
    }

    private void UpdatePath()
    {
        collisionWithGround = false;
        collisionWithWall = false;
        vertexArcList.Clear();
        velocity = Quaternion.AngleAxis(-MaxAngle,transform.right)*transform.forward*strenght;//??
        RaycastHit hit;
        Vector3 pos = transform.position;
        vertexArcList.Add(pos);
        while (!collisionWithGround && vertexArcList.Count < maxVertex)
        {
            Vector3 newPos = pos + velocity * vertexDelta + 0.5f * Physics.gravity * vertexDelta * vertexDelta;
            velocity = velocity + Physics.gravity * vertexDelta;
            vertexArcList.Add(newPos);
            //Aqui miramos si la linea entre la anterior y la actual choca con el suelo
            if (Physics.Linecast(pos, newPos, out hit, WallLayer))//Si collisiona con una pared no teletransportamos
            {
                collisionWithWall = true;
                collisionWithGround = true;
                groundPos = hit.point;
                lastNormal = hit.normal;
            }
            if (Physics.Linecast(pos, newPos, out hit,collisionLayers))// 4o argumento los layers a aceptar
            {
                //Si es asi activamos la señal y actualizamos la posición a teletransportarnos
                collisionWithGround = true;
                groundPos = hit.point;
                lastNormal = hit.normal;
                
            }
            pos = newPos;
        }
        positionMarker.SetActive(collisionWithGround);

        if (collisionWithGround)
        {
            positionMarker.transform.position = groundPos + lastNormal * 0.1f;
            positionMarker.transform.LookAt(groundPos);
            //if (collisionWithWall) {Cambiamos color }
        }

        arcRnderer.positionCount = vertexArcList.Count;
        arcRnderer.SetPositions(vertexArcList.ToArray());

    }
}

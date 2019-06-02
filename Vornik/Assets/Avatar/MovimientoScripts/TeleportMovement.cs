using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMovement : MonoBehaviour {

    public GameObject Marker; //Señal del suelo
    public Transform BodyTransforma; // Objeto a mover
    public LayerMask collisionLayers; //Layers correctos
    public LayerMask WallLayer;
    public float MaxAngle = 45f; //Maximo angulo del arco
    public float strenght = 10f; //Fuerza de salida del arco

    public bool collisionWithWall = false;

    private int _maxVertex= 100;

    private float _vertexDelta = 0.08f; //??
    [SerializeField] private LineRenderer arcRnderer;
    private Vector3 _velocity;
    private Vector3 _groundPos;
    private Vector3 _lastNormal;
    private bool _collisionWithGround = false;
    private List<Vector3> _vertexArcList = new List<Vector3>();
    private bool _display = false;

    public void Awake()
    {
        arcRnderer = GetComponent<LineRenderer>();
        arcRnderer.enabled=false;
        //Marker.SetActive(false);
    }

    public void SetDisplay(bool active)//Activar y desactivar el renderizado del arco
    {
        arcRnderer.enabled = active;
        //Marker.SetActive(active);
        _display = active;
    }

    public void Teleport()//Transporta el cuerpo a el sitio marcado
    {
        if (_collisionWithGround && !collisionWithWall)
        {
            BodyTransforma.position = _groundPos + _lastNormal ;
        }
    }

    private void FixedUpdate() //Si se muestra el renderizado del arco lo actualiza
    {
        if (_display)
        {
            UpdatePath();
        }
    }

    private void UpdatePath()
    {
        _collisionWithGround = false;
        collisionWithWall = false;
        _vertexArcList.Clear();
        _velocity = Quaternion.AngleAxis(-MaxAngle,transform.right)*transform.forward*strenght;//??
        RaycastHit hit;
        Vector3 pos = transform.position;
        _vertexArcList.Add(pos);
        while (!_collisionWithGround && _vertexArcList.Count < _maxVertex)
        {
            Vector3 newPos = pos + _velocity * _vertexDelta + 0.5f * Physics.gravity * _vertexDelta * _vertexDelta;
            _velocity = _velocity + Physics.gravity * _vertexDelta;
            _vertexArcList.Add(newPos);
            //Aqui miramos si la linea entre la anterior y la actual choca con el suelo
            if (Physics.Linecast(pos, newPos, out hit, WallLayer))//Si collisiona con una pared no teletransportamos
            {
                collisionWithWall = true;
                _collisionWithGround = true;
                _groundPos = hit.point;
                _lastNormal = hit.normal;
            }
            if (Physics.Linecast(pos, newPos, out hit,collisionLayers))// 4o argumento los layers a aceptar
            {
                //Si es asi activamos la señal y actualizamos la posición a teletransportarnos
                _collisionWithGround = true;
                _groundPos = hit.point;
                _lastNormal = hit.normal;
                
            }
            pos = newPos;
        }
        //Marker.SetActive(_collisionWithGround);
        /*
        if (_collisionWithGround)
        {
            Marker.transform.position = _groundPos + _lastNormal * 0.1f;
            Marker.transform.LookAt(_groundPos);
            //if (collisionWithWall) {Cambiamos color }
        }*/

        arcRnderer.positionCount = _vertexArcList.Count;
        arcRnderer.SetPositions(_vertexArcList.ToArray());

    }
}

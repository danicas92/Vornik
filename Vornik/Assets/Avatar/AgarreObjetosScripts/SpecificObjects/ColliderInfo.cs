using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderInfo : MonoBehaviour
{
    [SerializeField] private int colliderIndentificador;

    public int GetColliderIdentificador() { return colliderIndentificador; }
}

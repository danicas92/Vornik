using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateGame : MonoBehaviour
{
    public string stage;
    //Util para más atributos
    public string GetStage() { return stage; }
    public void SetStage(string stage) { this.stage = stage; }

}

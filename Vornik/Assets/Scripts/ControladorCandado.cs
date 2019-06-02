using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCandado : MonoBehaviour
{
    [SerializeField] private ControladorRueda controladorRueda;
    [SerializeField] private int posCorrect;
    [SerializeField] private ButtonInfo[] buttons;

    private int _actualPos = 0;
    private bool _imCorrect = false;

    public bool GetCorrent() => _imCorrect;

    public void Rota(bool direccion)
    {
        _actualPos += direccion ? _actualPos==3? -3:1 : _actualPos == 0 ? 3 : -1;
        transform.Rotate(new Vector3(0,direccion? 90:-90,0));
        CheckISCorrect();
        controladorRueda.Check();
    }

    private void CheckISCorrect()
    {
        if (posCorrect == _actualPos)
            _imCorrect = true;
        else
            _imCorrect = false;
    }

    private void OnDisable()
    {
        foreach (var button in buttons)
        {
            button.enabled = false;
        }
    }
}

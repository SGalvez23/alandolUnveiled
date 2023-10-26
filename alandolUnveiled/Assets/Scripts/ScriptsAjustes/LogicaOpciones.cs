using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaOpciones : MonoBehaviour
{
    public ControladorOpciones MenuAjustes;
    void Start()
    {
        MenuAjustes = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControladorOpciones>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarOpciones();
        }
    }

    public void MostrarOpciones()
    {
        MenuAjustes.pantallaOpciones.SetActive(true);
    }
}

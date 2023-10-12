using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AjustePantalla : MonoBehaviour
{
    public Toggle toggle;
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones; //Todas las resoluciones que tiene el ordenador

    void Start()
    {
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void RevisarResolucion()
    {
        resoluciones = Screen.resolutions; //guarda todas las resoluciones del ordenador
        resolucionesDropDown.ClearOptions(); //borra las opciones que estan de cajon
        List<string> opciones = new List<string>(); //Crea una lista nueva donde guarda el tamaño de la resolucion para mostrarlo
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++) //El for depende de cuantas resoluciones tenga el ordenador
        {
            string opcion = resoluciones[i].width + " x " + resoluciones[i].height; //Ejemplo 1920 x 1080
            opciones.Add(opcion);

            //Revisa si la opcion es la que se tiene actualmente en el juego y guardarla
            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width && resoluciones[i].height == Screen.currentResolution.height)
            {
                resolucionActual = i;
            }
        }
        resolucionesDropDown.AddOptions(opciones); //agrega las opciones guardas en la lista 
        resolucionesDropDown.value = resolucionActual; //detecta en que resolucion nos encontramos
        resolucionesDropDown.RefreshShownValue(); //actualiza la lista

        resolucionesDropDown.value = PlayerPrefs.GetInt("numeroResolucion", 0);
    }

    //funcion cuando cambiemos la resolucion
    public void CambiarResolucion(int indiceResolucion)
    {
        PlayerPrefs.SetInt("numeroResolucion", resolucionesDropDown.value);

        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen); //esto es lo que cambia la resolucion
    }
}

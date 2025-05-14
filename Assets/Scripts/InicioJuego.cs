using UnityEngine;
using TMPro;
using System.Collections;

public class InicioJuego : MonoBehaviour
{
    public GameObject panelInicio;
    public TMP_Text textoCuentaAtras;
    public GameManager gameManager;
    private bool enBucle = false;

    private void Start()
    {
        // Comprobar si estamos en el modo bucle (puedes usar una variable estática en MenuPrincipal o buscar el MenuPrincipal activo)
        MenuPrincipal menuPrincipal = FindObjectOfType<MenuPrincipal>();
        if (menuPrincipal != null && menuPrincipal.bucle)
        {
            enBucle = true;
            // Si estamos en bucle, iniciar la cuenta atrás inmediatamente
            EmpezarCuentaAtrasAutomatico();
        }
        else
        {
            enBucle = false;
            // Si no estamos en bucle, el botón podría activar EmpezarCuentaAtras
            if (panelInicio != null)
            {
                panelInicio.SetActive(true); // Activar el panel de inicio si existe
            }
            textoCuentaAtras.gameObject.SetActive(false); // Asegurarse de que el texto de la cuenta atrás esté oculto
        }
    }

    public void EmpezarCuentaAtrasAutomatico()
    {
        if (panelInicio != null)
        {
            panelInicio.SetActive(false); // Ocultar el panel de inicio si existe
        }
        StartCoroutine(CuentaAtras());
    }


    public void EmpezarCuentaAtras()
    {
        panelInicio.SetActive(false);
        StartCoroutine(CuentaAtras());
    }

    IEnumerator CuentaAtras()
    {
        textoCuentaAtras.gameObject.SetActive(true);

        textoCuentaAtras.text = "3";
        yield return new WaitForSeconds(1f);

        textoCuentaAtras.text = "2";
        yield return new WaitForSeconds(1f);

        textoCuentaAtras.text = "1";
        yield return new WaitForSeconds(1f);

        textoCuentaAtras.gameObject.SetActive(false);

        gameManager.EmpezarJuego();
    }
}


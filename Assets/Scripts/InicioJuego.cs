using UnityEngine;
using TMPro;
using System.Collections;

public class InicioJuego : MonoBehaviour
{
    public GameObject panelInicio;
    public TMP_Text textoCuentaAtras;
    public GameManager gameManager;

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


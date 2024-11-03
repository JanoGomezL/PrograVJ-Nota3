using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Personaje : MonoBehaviour
{
    public int vidaMaxima = 100;
    private int vidaActual;

    [Range(0, 100)] public float porcentajeArmadura;

    public TMP_Text vidaTexto;
    public TMP_Text armaduraTexto;
    public Sprite[] spritesVida;
    public Image hudVidaImage;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarTextoVida();
        ActualizarTextoArmadura();
        ActualizarSpriteVida();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RecibirDaño(15);
        }
    }

    public void RecibirDaño(int daño)
    {
        int dañoReducido = CalcularDañoConArmadura(daño);
        vidaActual -= dañoReducido;

        Debug.Log($"Daño recibido: {dañoReducido}. Vida actual: {vidaActual}"); // Confirma que el jugador ha recibido daño

        if (vidaActual < 0)
        {
            vidaActual = 0;
        }

        ActualizarTextoVida();
        ActualizarSpriteVida();
    }

    private int CalcularDañoConArmadura(int daño)
    {
        float reducción = daño * (porcentajeArmadura / 100f);
        int dañoFinal = Mathf.RoundToInt(daño - reducción);
        return dañoFinal;
    }

    public void Curar(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
        ActualizarTextoVida();
        ActualizarSpriteVida();
    }

    private void ActualizarTextoVida()
    {
        vidaTexto.text = vidaActual.ToString() + "%";
    }

    private void ActualizarTextoArmadura()
    {
        armaduraTexto.text = porcentajeArmadura.ToString("F0") + "%";
    }

    public void AjustarArmadura(float nuevoPorcentaje)
    {
        porcentajeArmadura = nuevoPorcentaje;
        ActualizarTextoArmadura();
    }

    private void ActualizarSpriteVida()
    {
        int index = (vidaMaxima - vidaActual) / 15;
        index = Mathf.Clamp(index, 0, spritesVida.Length - 1);
        hudVidaImage.sprite = spritesVida[index];
    }
}

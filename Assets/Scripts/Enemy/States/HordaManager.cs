using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HordaManager : MonoBehaviour
{
    public GameObject[] enemigosPrefabs; // Asigna los prefabs de enemigos desde el inspector
    public Transform[] puntosDeSpawn; // Puntos de spawn definidos en la escena
    public int tiempoInicial = 60;
    public int decremento = 5;
    public int tiempoMinimo = 10;
    public int maxZombiesPorHorda = 1; // Número de enemigos por horda

    private int tiempoActual;
    private int totalEnemigosSpawneados = 0;

    void Start()
    {
        tiempoActual = tiempoInicial;
        StartCoroutine(GenerarHordas());
    }

    IEnumerator GenerarHordas()
    {
        while (true)
        {
            SpawnearHorda();
            yield return new WaitForSeconds(tiempoActual);

            if (tiempoActual > tiempoMinimo)
            {
                tiempoActual -= decremento;
            }
            else
            {
                tiempoActual = tiempoMinimo;
            }
        }
    }

    void SpawnearHorda()
    {
        if (enemigosPrefabs.Length == 0 || puntosDeSpawn.Length == 0)
        {
            Debug.LogError("No se han asignado enemigos o puntos de spawn.");
            return;
        }

        for (int i = 0; i < maxZombiesPorHorda; i++)
        {
            int tipoEnemigo = Random.Range(0, enemigosPrefabs.Length);
            if (enemigosPrefabs[tipoEnemigo] != null)
            {
                Transform puntoSpawn = ObtenerPuntoAleatorio();
                GameObject enemigoInstanciado = Instantiate(enemigosPrefabs[tipoEnemigo], puntoSpawn.position, Quaternion.identity);

                // Inicializar el enemigo
                InicializarEnemigo(enemigoInstanciado);
                totalEnemigosSpawneados++;
                Debug.Log("Enemigo instanciado: " + enemigoInstanciado.name);
            }
            else
            {
                Debug.LogWarning("El prefab de enemigo es nulo. Verifica que el objeto no haya sido destruido.");
            }
        }

        Debug.Log($"Horda generada: {maxZombiesPorHorda} enemigo(s) spawneado(s). Total de enemigos en el campo: {totalEnemigosSpawneados}");
    }

    void InicializarEnemigo(GameObject enemigo)
    {
        Renderer renderer = enemigo.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.enabled = true; // Asegura que el Renderer esté habilitado
        }
        else
        {
            Debug.LogWarning("El enemigo instanciado no tiene un Renderer.");
        }

        enemigo.SetActive(true); // Asegura que el enemigo esté activo
    }

    Transform ObtenerPuntoAleatorio()
    {
        return puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];
    }
}

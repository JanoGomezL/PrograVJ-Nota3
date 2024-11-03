using UnityEngine;
using System.Collections;

public class HordaManager : MonoBehaviour
{
    public GameObject[] enemigosPrefabs; // Asigna los prefabs de Enemy1 y Enemy2 desde el inspector
    public Transform[] puntosDeSpawn; // Puntos de spawn definidos en la escena
    public int tiempoInicial = 60;
    public int decremento = 5;
    public int tiempoMinimo = 10;
    public int maxZombiesPorHorda = 1; // Cambiado para spawnear solo un enemigo por horda

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
            Instantiate(enemigosPrefabs[tipoEnemigo], ObtenerPuntoAleatorio().position, Quaternion.identity);
            totalEnemigosSpawneados++;
        }

        Debug.Log($"Horda generada: {maxZombiesPorHorda} enemigo(s) spawneado(s). Total de enemigos en el campo: {totalEnemigosSpawneados}");
    }

    Transform ObtenerPuntoAleatorio()
    {
        return puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)];
    }
}

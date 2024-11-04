using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public ParticleSystem Disparo;
    public AudioSource sonidoDisparo;

    public GameObject balaPrefab;
    public Transform puntoDisparo;
    public float velocidadBala = 20f;
    public float distanciaMaxima = 50f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Disparo.Play();
        sonidoDisparo.Play();

        GameObject bala = Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
        Rigidbody rb = bala.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = puntoDisparo.forward * velocidadBala;
        }

        Destroy(bala, distanciaMaxima / velocidadBala);
    }

    IEnumerator DetenerParticulas()
    {
        yield return new WaitForSeconds(Disparo.main.duration);
        Disparo.Stop();
    }
}

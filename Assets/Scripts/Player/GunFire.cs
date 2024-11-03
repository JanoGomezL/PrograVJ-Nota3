using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{
    public ParticleSystem Disparo;
    public AudioSource sonidoDisparo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Disparo.Play();          // Activa las partículas
        sonidoDisparo.Play();     // Reproduce el sonido de disparo
    }

    IEnumerator DetenerParticulas()
    {
        yield return new WaitForSeconds(Disparo.main.duration);
        Disparo.Stop();
    }
}

using UnityEngine;

public class EnemyChase : EnemyState
{
    private float tiempoEntreAtaques = 5.0f; // Tiempo en segundos entre ataques
    private float proximoAtaqueTiempo; // Tiempo para el próximo ataque

    public EnemyChase(EnemyController controller) : base(controller)
    {
        Transition transitionChaseToIdle = new Transition(
            isValid: () => {
                float dist = Vector3.Distance(
                    m_Controller.GetPlayer().position,
                    m_Controller.transform.position
                );
                return dist >= m_Controller.GetDistanceToChase();
            },
            getNextState: () => m_Controller.IdleState
        );
        Transitions.Add(transitionChaseToIdle);
    }

    public override void OnFinish()
    {
        Debug.Log($"Chase: OnFinish {m_Controller.GetDistanceToChase()}");
    }

    public override void OnStart()
    {
        Debug.Log("Chase: OnStart");
        proximoAtaqueTiempo = Time.time + tiempoEntreAtaques; // Configura el primer tiempo de ataque
    }

    public override void OnUpdate()
    {
        m_Controller.GetAgent().SetDestination(m_Controller.GetPlayer().position);

        float distanciaAlJugador = Vector3.Distance(
            m_Controller.GetPlayer().position,
            m_Controller.transform.position
        );

        // Verificamos si el enemigo es el más cercano y si el tiempo de ataque ha pasado
        if (distanciaAlJugador <= 1.5f && Time.time >= proximoAtaqueTiempo)
        {
            if (EsElEnemigoMasCercano())
            {
                // Actualizamos el tiempo para el siguiente ataque
                proximoAtaqueTiempo = Time.time + tiempoEntreAtaques;
                Debug.Log($"Daño aplicado. Siguiente ataque permitido en: {proximoAtaqueTiempo}");
                Debug.Log("Aplicando daño al jugador desde el enemigo más cercano");
                PlayerController.Instance.RecibirDañoDesdeEnemigo(1);

                
            }
        }

        if (EsElEnemigoMasCercano() && Time.time < proximoAtaqueTiempo)
        {
            Debug.Log($"Esperando para el próximo ataque. Tiempo restante: {proximoAtaqueTiempo - Time.time}");
        }
    }

    private bool EsElEnemigoMasCercano()
    {
        EnemyController[] enemigos = GameObject.FindObjectsOfType<EnemyController>();
        float distanciaMinima = Mathf.Infinity;
        EnemyController enemigoMasCercano = null;

        foreach (var enemigo in enemigos)
        {
            float distancia = Vector3.Distance(
                enemigo.transform.position,
                PlayerController.Instance.transform.position
            );
            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo;
            }
        }

        return enemigoMasCercano == m_Controller;
    }
}

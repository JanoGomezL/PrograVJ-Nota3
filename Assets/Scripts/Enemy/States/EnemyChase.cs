
using UnityEngine;

public class EnemyChase : EnemyState
{
    public EnemyChase(EnemyController controller) : base(controller)
    {
        // Configurando transiciones
        Transition transitionChaseToIdle = new Transition(
            isValid: () => {
                float dist = Vector3.Distance(
                    m_Controller.GetPlayer().position,
                    m_Controller.transform.position
                );

                return dist >= m_Controller.GetDistanceToChase();
            },
            getNextState: () => {
                return m_Controller.IdleState;
            }
        );
        Transitions.Add(transitionChaseToIdle);
    }

    public override void OnFinish()
    {
        Debug.Log($"Chase: OnFinish { m_Controller.GetDistanceToChase() }");
    }

    public override void OnStart()
    {
        Debug.Log("Chase: OnStart");
    }

    public override void OnUpdate()
    {
        /* var dir = m_Controller.GetPlayer().position - m_Controller.transform.position;
        dir.y = 0f;
        dir.Normalize();

        m_Controller.transform.position += dir * 1f * Time.deltaTime; */
        m_Controller.GetAgent()
            .SetDestination(m_Controller.GetPlayer().position);
    }
}

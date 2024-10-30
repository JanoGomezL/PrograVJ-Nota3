using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdle : EnemyState
{
    public EnemyIdle(EnemyController controller) : base(controller)
    {
        Transition transitionIdleToChase = new Transition(
            isValid: () => {
                float dist = Vector3.Distance(
                    m_Controller.GetPlayer().position,
                    m_Controller.transform.position
                );
                
                return dist < m_Controller.GetDistanceToChase();
            },
            getNextState: () => {
                return m_Controller.ChaseState;
            }
        );

        Transitions.Add(transitionIdleToChase);
    }

    public override void OnFinish()
    {
        Debug.Log("Idle: OnFinish");
    }

    public override void OnStart()
    {
        Debug.Log("Idle: OnStart");
    }

    public override void OnUpdate()
    {}
}

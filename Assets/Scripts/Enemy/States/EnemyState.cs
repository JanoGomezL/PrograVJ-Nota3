
using System.Collections.Generic;

public abstract class EnemyState
{
    protected EnemyController m_Controller;
    public List<Transition> Transitions;

    public EnemyState(EnemyController controller)
    {
        m_Controller = controller;
        Transitions = new List<Transition>();
    }

    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnFinish();
}

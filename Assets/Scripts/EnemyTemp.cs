using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTemp : MonoBehaviour
{
    public Transform Player;
    private NavMeshAgent m_Agent;

    private void Awake() 
    {
        m_Agent = GetComponent<NavMeshAgent>();
    }

    private void Update() 
    {
        m_Agent.SetDestination(Player.position);    
    }
}

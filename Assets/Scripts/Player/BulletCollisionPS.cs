using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisionPS : MonoBehaviour
{
    [SerializeField]
    private float m_Lifetime = 5f;
    private float m_Timer = 0f;

    private ParticleSystem m_BulletCollisionPS;

    private void Awake() {
        m_BulletCollisionPS = GetComponent<ParticleSystem>();
    }

    private void Update() 
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= m_Lifetime)
        {
            Destroy(gameObject);
            /* var shape = m_BulletCollisionPS.shape;
            shape.radius += 0.05f;  
            m_Timer = 0f;*/
        }
        
    }
}

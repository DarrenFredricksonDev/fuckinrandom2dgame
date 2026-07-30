using UnityEngine;
using System.Collections.Generic;

public class ParticleDamage : MonoBehaviour
{
    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int count = ps.GetCollisionEvents(other, collisionEvents);
        for (int i = 0; i < count; i++)
        {
            var evt = collisionEvents[i];
            // send whatever you want the receiver to get (e.g., intersection point or whole event)
            other.SendMessage("OnHitByParticle", evt, SendMessageOptions.DontRequireReceiver);
        }
    }
}
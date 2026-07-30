using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    void OnParticleCollision(GameObject other)
    {
        other.SendMessage("OnHitByParticle", data, DontRequireReceiver);
    }
}

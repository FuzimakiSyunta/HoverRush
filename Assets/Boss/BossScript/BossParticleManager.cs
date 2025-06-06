using UnityEngine;

public class BossParticleManager : MonoBehaviour
{
    [Header("パーティクルPrefab")]
    [SerializeField] private ParticleSystem damageParticle;
    [SerializeField] private ParticleSystem lazerDamageParticle;
    [SerializeField] private ParticleSystem deathParticlePrefab;

    public void PlayDamageEffect(Vector3 position)
    {
        if (damageParticle != null)
        {
            ParticleSystem instance = Instantiate(damageParticle, position, Quaternion.identity);
            instance.Play();
            Destroy(instance.gameObject, 5f);
        }
    }

    public void PlayLazerDamageEffect(Vector3 position)
    {
        if (lazerDamageParticle != null)
        {
            ParticleSystem instance = Instantiate(lazerDamageParticle, position, Quaternion.identity);
            instance.Play();
            Destroy(instance.gameObject, 5f);
        }
    }

    public void PlayDeathEffect(Vector3 position)
    {
        if (deathParticlePrefab != null)
        {
            GameObject particle = Instantiate(deathParticlePrefab.gameObject, position, Quaternion.identity);
            Destroy(particle, 2.5f);
        }
    }
}

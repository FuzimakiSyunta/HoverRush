using UnityEngine;

public class BossAudioManager : MonoBehaviour
{
    [Header("ÉTÉEÉìÉhê›íË")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;

    public void PlayDamageSound()
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
    }
}

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Stats stats = collision.GetComponentInChildren<Stats>();

            if (stats != null)
            {
                stats.DecreaseHealth(9999); // cho mất hết máu
                if (audioManager != null)
                    audioManager.DeathSource();
            }
        }
    }
}

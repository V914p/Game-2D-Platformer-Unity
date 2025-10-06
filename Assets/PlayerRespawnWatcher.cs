using System.Collections;
using UnityEngine;

public class PlayerRespawnWatcher : MonoBehaviour
{
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float respawnDelay = 1f;

    private bool wasActive = true;

    void Update()
    {
        if (playerObject == null) return;

        if (wasActive && !playerObject.activeInHierarchy)
        {
            Debug.Log("Player was deactivated. Respawning...");
            StartCoroutine(RespawnPlayer());
        }

        wasActive = playerObject.activeInHierarchy;
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnDelay);

        playerObject.SetActive(true);
        Debug.Log("Player reactivated.");

        PlayerHealth playerHealth = playerObject.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.RespawnAtCheckpoint();
        }
    }
}

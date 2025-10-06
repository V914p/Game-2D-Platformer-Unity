using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Vector3 checkpointPos;   // Lưu checkpoint
    [SerializeField] private int maxHealth = 100;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Stats stats;

    [Header("UI")]
    public HealthBar healthBar;

    [Header("Events")]
    public UnityEvent OnDeath;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        stats = GetComponentInChildren<Stats>();
    }

    private void Start()
    {
        // checkpoint mặc định là vị trí spawn ban đầu
        checkpointPos = transform.position;

        if (stats != null)
        {
            // lắng nghe sự kiện từ Stats
            stats.OnHealthChanged += HandleHealthChanged;
            stats.OnHealthZero += HandleDeath;

            // khởi tạo HP đầy
            stats.ResetHealth();
        }

        // sync UI lần đầu
        if (healthBar != null)
            healthBar.UpdateBar(maxHealth, maxHealth);
    }

    /// <summary>
    /// Player bị quái hoặc trap gây sát thương
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (damage <= 0 || stats == null) return;

        stats.DecreaseHealth(damage);
        Debug.Log($"Player took {damage} damage");
    }

    /// <summary>
    /// Cập nhật checkpoint mới khi player chạm checkpoint
    /// </summary>
    public void UpdateCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPos = newCheckpoint;
    }

    /// <summary>
    /// Respawn player tại checkpoint, hồi máu đầy
    /// </summary>
    public void RespawnAtCheckpoint()
    {
        // đưa về vị trí checkpoint
        transform.position = checkpointPos;

        // reset máu
        stats?.ResetHealth();

        // bật lại body + sprite
        if (rb != null) rb.bodyType = RigidbodyType2D.Dynamic;
        if (sr != null) sr.enabled = true;

        Debug.Log("Player respawned at checkpoint: " + checkpointPos);
    }

    // ------------------- PRIVATE HANDLERS -------------------

    private void HandleHealthChanged(float current, float max)
    {
        if (healthBar != null)
            healthBar.UpdateBar((int)current, (int)max);
    }

    private void HandleDeath()
    {
        OnDeath?.Invoke();
        gameObject.SetActive(false); // tắt player → Watcher sẽ re-enable
        Debug.Log("Player died (HandleDeath invoked).");
    }
}

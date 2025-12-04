using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : PlayerSystem
{
    private Animator animator;
    [Header("Health Settings")]
    public int health = 3;
    public float damageCooldown = 1f;
    public bool debugInvincibility = false;
    [Header("Audio")]
    [SerializeField] private AudioSource AudioSource;

    private float damageTimer;
    private int damage = 1;
    private bool canTakeDamage = true;
    private bool hasIframes = false; // from roll system



    void Start()
    {
        animator = GetComponent<Animator>();
    }

  
    private void TakeDamage(bool isRolling)
    {
        // If rolling, grant temporary invincibility
        hasIframes = isRolling;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Enemy") || other.CompareTag("EnemyBullet")) && canTakeDamage && !hasIframes && !debugInvincibility)
        {
            Debug.Log("Player overlapped with an Enemy!");
            health -= damage;
            player.ID.events.OnHealthChange?.Invoke();
            Debug.Log("I took damage! My health is at: " + health);

            damageTimer = damageCooldown;
            animator.SetBool("Damaged", true);
            canTakeDamage = false;
            AudioSource.Play();
        }
    }



    void Update()
    {
        // handle damage cooldown
        if (!canTakeDamage)
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0f)
            {
                canTakeDamage = true;
                animator.SetBool("Damaged", false);
            }
        }

        if (health <= 0)
        {
            animator.SetBool("isDead", true);
            canTakeDamage = false;
            player.ID.events.OnDeathStateChange?.Invoke(true);
            SceneManager.LoadScene("RetryScreen");
        }
    }

    private void OnEnable()
    {
        player.ID.events.OnRollingStateChange += TakeDamage;
    }

    private void OnDisable()
    {
        player.ID.events.OnRollingStateChange -= TakeDamage;
    }

    public int GetHealth()
    {
        return health;
    }
}

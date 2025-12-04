using UnityEngine;
using UnityEngine.UIElements;


public class AimAndShoot2D : MonoBehaviour
{
    public enum ControllerClass { Player, AI }

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 20f;
    public float fireRate = 0.2f; // seconds between shots
    public ControllerClass control;
    public AudioSource audioSource;
    public AudioClip[] shootSounds;

    private Transform target;
    private float nextFireTime = 0f;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        
    }
    
    
    void Update()
    {
        if (control == ControllerClass.Player && !PauseMenu.isPaused)
        {
            AimAtMouse();
            if (Input.GetKey(KeyCode.Mouse0) && Time.time >= nextFireTime && control == ControllerClass.Player)
            {
                Shoot();
                nextFireTime = Time.time + fireRate ;
            }
        }
        else if (control == ControllerClass.AI && Time.time >= nextFireTime)
        {
            Vector2 shootDirection = (target.position - transform.position).normalized;

            // If your bullet sprite faces right (default):
            float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(0, 0, angle);

            // Instantiate bullet
            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.tag = "EnemyBullet";

            // Apply velocity
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.linearVelocity = shootDirection * bulletSpeed;
            float randomFireRate = Random.Range(2.0f, 5.0f);
            nextFireTime = Time.time + randomFireRate ;
        }

    }

    void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = firePoint.right * bulletSpeed;
        PlayRandomSoundEffect();
    }

    public void PlayRandomSoundEffect()
    {
        if (shootSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, shootSounds.Length);
            audioSource.PlayOneShot(shootSounds[randomIndex]);
        }
    }
}
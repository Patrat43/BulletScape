using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;
    public int pointsOnHit = 10; // Points earned per enemy destroyed

    private PointSystem pointSystem;
    private bool hasHit = false;

    void Start()
    {
        Destroy(gameObject, lifeTime); // destroy after a few seconds
        // Find the player's PointSystem automatically
        pointSystem = FindAnyObjectByType<PointSystem>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return; // already hit something, ignore further collisions
        if (collision.CompareTag("Enemy") && this.tag != "EnemyBullet")
        {   
            hasHit = true;

            // Add points through the player's point system
            if (pointSystem != null)
                pointSystem.AddPoints(pointsOnHit);

            // Destroy enemy and bullet
            Destroy(collision.gameObject); //This destroys the enemy
            Destroy(gameObject); //This destroys the bullet
        }
    }
}
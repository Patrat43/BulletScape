using UnityEngine;
using System;

public class AiChase : MonoBehaviour
{

    public GameObject targetPlayer;
    public float chaseSpeed;
    private Animator animator;

    public event Action onDeath;

    void OnDestroy()
    {
        onDeath?.Invoke();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();

        if (targetPlayer == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                targetPlayer = playerObj;
            }
            else
            {
                Debug.LogWarning($"{name} couldn't find player!");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Find what direction the player is in and then move towards them
        Vector2 direction = (targetPlayer.transform.position - transform.position).normalized; /*This can be moved to RandomMovement script an change the
                                                                                                 orientation of the animation*/
        if (direction.x < 0)
        {
            animator.SetInteger("Direction", 0); //Left
        }
        else if (direction.x > 0)
        {
            animator.SetInteger("Direction", 1); //Right
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPlayer.transform.position, chaseSpeed * Time.deltaTime);
    }
}

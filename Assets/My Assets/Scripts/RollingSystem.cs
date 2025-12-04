using UnityEngine;
using Cainos.PixelArtTopDown_Basic;

public class RollingSystem : PlayerSystem
{

    public float rollForce = 8f; //Force of the Roll
    public float rollDuration = 0.3f; //Duration Roll
    public float rollCooldown = 1f; //time between rolls

    private bool isRolling = false;
    private float rollTimer = 0f;
    private float cooldownTimer = 0f;


    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// This method initiates the roll action if not on cooldown.
    /// </summary>
    private void Roll()
    {
        if (cooldownTimer <= 0f && isRolling == false)
        {
            isRolling = true;
            rollTimer = rollDuration;
            player.ID.events.OnRollingStateChange?.Invoke(isRolling); // Notify other systems of rolling state change
            animator.SetBool("isRolling", true);
            
            GetComponent<Rigidbody2D>().linearVelocity = GetComponent<TopDownCharacterController>().lastMoveDir * rollForce; // Apply roll forcein the last movement direction
        }
    }

    private void RollLength()
    {
        if (isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0.0f)
            {
                isRolling = false;
                player.ID.events.OnRollingStateChange?.Invoke(isRolling);
                cooldownTimer = rollCooldown;
                animator.SetBool("isRolling", false);
            }
        }
    }

    private void RollCooldown()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        
        RollLength();
        RollCooldown();
    }

    private void OnEnable()
    {
        player.ID.events.OnRoll += Roll;
    }

    private void OnDisable()
    {
        player.ID.events.OnRoll -= Roll;
      
    }

}
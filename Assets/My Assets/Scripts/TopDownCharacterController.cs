using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : PlayerSystem
    {
        public float speed;

        private bool isRolling;
        private Animator animator;
        private Rigidbody2D rb;
        private bool isPlayerDead = false;

        
        public Vector2 lastMoveDir = Vector2.down;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            Vector2 dir = Vector2.zero;
           
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 1);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
            }

            if (dir != Vector2.zero)
            {
                lastMoveDir = dir.normalized;
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.magnitude > 0);
            
            //Only MOVE when NOT rolling and alive
            if (isPlayerDead == false && isRolling == false)
            {
                    rb.linearVelocity = speed * dir;
            }

            if (isPlayerDead)
            {
                rb.linearVelocity = 0 * dir;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                player.ID.events.OnRoll?.Invoke();
            }
        }

        private void HandleRollingState(bool rolling)
        {
            isRolling = rolling;
        }

        private void HandleDeathState(bool isPlayerDead_)
        {
            isPlayerDead = isPlayerDead_;
        }

        private void OnEnable()
        {
            player.ID.events.OnRollingStateChange += HandleRollingState;
            player.ID.events.OnDeathStateChange += HandleDeathState;
        }

        private void OnDisable()
        {
            player.ID.events.OnRollingStateChange -= HandleRollingState;
            player.ID.events.OnDeathStateChange -= HandleDeathState;
        }
    }
}

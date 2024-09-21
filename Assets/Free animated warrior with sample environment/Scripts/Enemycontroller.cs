using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarrior : MonoBehaviour
{
    public Transform player;        // Reference to the player's transform
    public float detectionRange = 5f;   // Range at which the enemy detects the player
    public float attackRange = 2f;      // Range at which the enemy performs a jump attack
    public float movementSpeed = 5f;    // Speed at which the enemy moves towards the player

    private Animator animator;          // Reference to the animator component
    private bool isDead = false;        // Flag indicating if the enemy is dead

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);

                if (distanceToPlayer <= attackRange)
                {
                    // Rotate to face the player
                    Vector3 direction = player.position - transform.position;
                    direction.y = 0f;
                    transform.rotation = Quaternion.LookRotation(direction);

                    // Perform jump attack
                    animator.SetTrigger("JumpAttack");
                }
            }
        }
    }

    public void PerformAttack()
    {
        // Do whatever you want the enemy to do when attacking the player
        Debug.Log("Enemy is attacking the player.");
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        // Do whatever you want the enemy to do when taking damage
        Debug.Log("Enemy took " + damage + " damage.");

        // Here you can implement logic to reduce enemy health and handle death
        // ...
    }
}


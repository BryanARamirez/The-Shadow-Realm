using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy2 : MonoBehaviour
{
    public float speed = 5f; // The speed at which the enemy moves.
    public float sightDistance = 10f; // The distance at which the enemy can detect the player.
    public float rotationSpeed = 5f; // The speed at which the enemy rotates towards the player.
    public float fieldOfViewAngle = 60f; // The angle of the enemy's field of view.

    private Transform playerTransform; // The transform of the player.
    private bool canFollowPlayer = false; // Whether the enemy can follow the player.

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Check if the player is within sight distance and not in the player's line of sight.
        if (Vector3.Distance(transform.position, playerTransform.position) <= sightDistance &&
            !IsPlayerLookingAtEnemy())
        {
            canFollowPlayer = true;
        }
        else
        {
            canFollowPlayer = false;
        }

        // Move towards the player if the enemy can follow the player.
        if (canFollowPlayer)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    bool IsPlayerLookingAtEnemy()
    {
        Vector3 directionToEnemy = transform.position - playerTransform.position;

        if (Vector3.Angle(playerTransform.forward, directionToEnemy) <= fieldOfViewAngle / 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


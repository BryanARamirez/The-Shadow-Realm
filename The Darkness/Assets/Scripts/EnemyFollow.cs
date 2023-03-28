using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    /*public NavMeshAgent enemy;
    public Transform Player;
    public float speed = 25;
   

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);

    }
    */
    public NavMeshAgent enemy;
    public Transform player;    // reference to the player's Transform component
    public float followRange = 10f;    // range at which the enemy will start following the player
    public float speed = 5f;    // speed at which the enemy will move towards the player

    private void Update()
    {
        // check the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // if the player is within range, move towards the player
        if (distanceToPlayer <= followRange)
        {
            transform.LookAt(player.position);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InvisibleEnemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public float followRange = 20f;    // range at which the enemy will start following the player
    public float speed = 1f;    // speed at which the enemy will move towards the player
    [SerializeField] private Movement playerSneaking; //To get the sneaking bool from the Movement script to stop the enemy from chasing when sneakings
    private float invisibilityRange = 10f;
    [SerializeField] MeshRenderer invisibleEnemy;

    private void Update()
    {
        // check the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer >= invisibilityRange)
        {
            invisibleEnemy.enabled = false;
        }
        if (distanceToPlayer < invisibilityRange)
        {
            invisibleEnemy.enabled = true;
        }

        //Added if statements to stop the enemy from moving if the player sneaks
        if (playerSneaking.sneak == false)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            if (distanceToPlayer <= followRange)
            {
                transform.LookAt(player.position);
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        if (playerSneaking.sneak == true)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
}

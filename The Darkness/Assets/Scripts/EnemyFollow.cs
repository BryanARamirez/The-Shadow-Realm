using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    /*public NavMeshAgent enemy;
    public Transform Player;
    public float speed = 25;
    [SerializeField] private Movement playerSneaking;
   

    // Update is called once per frame
    void Update()
    {
        if (playerSneaking.sneak == false)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            enemy.SetDestination(Player.position);
        }
        if (playerSneaking.sneak == true)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }
    */
    public NavMeshAgent enemy;
    public Transform player;    // reference to the player's Transform component
    public float followRange = 10f;    // range at which the enemy will start following the player
    public float speed = 5f;    // speed at which the enemy will move towards the player
    [SerializeField] private Movement playerSneaking; //To get the sneaking bool from the Movement script to stop the enemy from chasing when sneakings

    private void Update()
    {
        // check the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

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

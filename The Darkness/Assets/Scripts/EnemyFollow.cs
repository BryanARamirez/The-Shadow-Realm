using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
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
}

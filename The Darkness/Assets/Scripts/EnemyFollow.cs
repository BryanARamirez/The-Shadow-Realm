using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform Player;
    public float speed = 25;
   

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(Player.position);

    }
}

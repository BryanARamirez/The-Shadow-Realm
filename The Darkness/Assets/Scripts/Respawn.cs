using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    public float maxFallDistance = 10f;

    void Update()
    {
        if (player.position.y < -maxFallDistance)
        {
            player.position = respawnPoint.position;
        }
    }
}

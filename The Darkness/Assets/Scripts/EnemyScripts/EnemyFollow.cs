using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    
    public NavMeshAgent enemy;
    public Transform player;    
    public float followRange = 10f;    // range at which the enemy will start following the player
    public float speed = 1f;    // speed at which the enemy will move towards the player
    [SerializeField] private Movement playerSneaking; //To get the sneaking bool from the Movement script to stop the enemy from chasing when sneakings
    public bool isStunned = false;
    [SerializeField] private PlayerInput playerInput;
    float yOffset = 1f;
    

    private void Update()
    {
        // check the distance between the player and the enemy
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        //Added if statements to stop the enemy from moving if the player sneaks
        if (playerSneaking.sneak == false) 
        {
            if (isStunned == false)
            {
                gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                if (distanceToPlayer <= followRange)
                {
                    Vector3 targetPosition = player.position + Vector3.up * yOffset;

                    transform.LookAt(targetPosition);
                    transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                }
            }
        }
        if (playerSneaking.sneak == true)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }


        int layer = 7;
        int layerMask = 1 << layer;
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;

        Vector3 origin = transform.position;
        Vector3 direction = transform.position;
        Debug.DrawRay(origin, this.transform.forward * 10f, Color.yellow);

        if (Physics.Raycast(ray, out hit, followRange, layerMask))
        {
            print("There is something in front of the object!");
            playerInput.detectedSense.enabled = true;
        }
        else
        {
            playerInput.detectedSense.enabled = false;
        }    
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StunTaser")
        {
            StartCoroutine(stunned());
        }
    }
    private IEnumerator stunned()
    {
        isStunned = true;
        for (int index = 0; index < 1f; index++)
        {
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            yield return new WaitForSeconds(3f);
        }
        isStunned = false;
        gameObject.GetComponent<NavMeshAgent>().isStopped = false;
    }
}

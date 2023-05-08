using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Elevator elevatorScript;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (elevatorScript.isDown)
            {
                elevatorScript.isGoingUp = true;
                elevatorScript.isGoingDown = false;
            }
            if (elevatorScript.isUp)
            {
                elevatorScript.isGoingDown = true;
                elevatorScript.isGoingUp = false;
            }
        }
    }
}

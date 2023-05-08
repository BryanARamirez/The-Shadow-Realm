using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject ElevatorGO;
    [SerializeField] private GameObject ElevatorUpPos;
    [SerializeField] private GameObject ElevatorDownPos;
    private float speed = 3;
    public bool isGoingUp;
    public bool isGoingDown;
    public bool isDown;
    public bool isUp;

    private void Awake()
    {
        isGoingUp = false;
        isGoingDown = false;
        isDown = true;
        isUp = false;
    }

    private void Update()
    {
        if(isGoingUp)
        {
            elevatorUp();
        }
        if(isGoingDown)
        {
            elevatorDown();
        }
        if(transform.position.y == ElevatorUpPos.transform.position.y)
        {
            isDown = false;
            isUp = true;
        }
        if (transform.position.y == ElevatorDownPos.transform.position.y)
        {
            isDown = true;
            isUp = false;
        }
    }

    public void elevatorUp()
    {
        transform.position = Vector3.MoveTowards(transform.position, ElevatorUpPos.transform.position, speed * Time.deltaTime);
    }
    public void elevatorDown()
    {
        transform.position = Vector3.MoveTowards(transform.position, ElevatorDownPos.transform.position, speed * Time.deltaTime);
    }
}

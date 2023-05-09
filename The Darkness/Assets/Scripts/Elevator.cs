using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject ElevatorGO;
    [SerializeField] private GameObject ElevatorUpPos;
    [SerializeField] private GameObject ElevatorDownPos;
    [SerializeField] private PlayerInput playerInputScript;
    private float speed = 3;
    public bool isGoingUp;
    public bool isGoingDown;
    public bool isDown;
    public bool isUp;
    public int currentLives;

    private void Awake()
    {
        isGoingUp = false;
        isGoingDown = false;
        isDown = true;
        isUp = false;
        currentLives = playerInputScript.lives;
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
        if(currentLives > playerInputScript.lives)
        {
            isGoingDown = true;
            isGoingUp = false;
            if (transform.position.y == ElevatorDownPos.transform.position.y)
            {
                isGoingDown = false;
                currentLives--;
            }
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

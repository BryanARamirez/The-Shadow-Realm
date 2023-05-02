using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject ElevatorGO;
    [SerializeField] private GameObject ElevatorUpPos;
    [SerializeField] private GameObject ElevatorDownPos;
    private float speed = 1;
    private float smoothTime = 0.5f;
    Vector3 currentVelocity;

    private void Update()
    {
        elevatorUp();
    }

    public void elevatorUp()
    {
        //transform.position = Vector3.MoveTowards(transform.position, ElevatorUpPos.transform.position, speed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, ElevatorUpPos.transform.position, ref currentVelocity, smoothTime);
    }
    public void elevatorDown()
    {
        //transform.position = Vector3.MoveTowards(transform.position, ElevatorDownPos.transform.position, speed * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, ElevatorDownPos.transform.position, speed * Time.deltaTime);
    }
}

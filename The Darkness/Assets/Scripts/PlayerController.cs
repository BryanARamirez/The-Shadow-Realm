using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private Rigidbody playerRigidBody;
    private bool isGrounded;
    private float jumpForce = 3;

    [SerializeField] private float moveSpeed;


    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerActionMap = new PlayerActionMap();
    }

    private void OnEnable()
    {
        playerActionMap.Enable();
    }

    private void OnDisable()
    {
        playerActionMap.Disable();
    }

    void Update()
    {
        transform.position += (Vector3)playerActionMap.Player.Movement.ReadValue<Vector3>() * moveSpeed * Time.deltaTime;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.5f))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
        if (Input.GetKey("space") && isGrounded)
        {
            playerRigidBody.AddForce(Vector3.up * jumpForce);
        }
    }
}

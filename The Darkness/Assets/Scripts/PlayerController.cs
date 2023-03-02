using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private Rigidbody playerRigidBody;

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
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (context.performed)
        {
            playerRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }

    void Update()
    {
        transform.position += (Vector3)playerActionMap.Player.Movement.ReadValue<Vector3>() * moveSpeed * Time.deltaTime;
    }
}

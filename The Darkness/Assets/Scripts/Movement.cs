using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    private float speed = 4f;
    private Vector2 horizontalInput;

    [SerializeField] private float jumpHeight = 3.5f;
    private bool jump;

    [SerializeField] private float gravity = -30f; // -9.81
    private Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    public bool sneak;
    [SerializeField] private MeshRenderer Hand;
    [SerializeField] private GameObject player;

    private float lastTime;
    private bool sprinting;

    private void Update()
    {
        //Helps handle gravity
        isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
        if (isGrounded)
        {
            verticalVelocity.y = 0;
        }

        Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
        controller.Move(horizontalVelocity * Time.deltaTime);

        if (jump)
        {
            if (isGrounded)
            {
                verticalVelocity.y = Mathf.Sqrt(-2f * jumpHeight * gravity);
            }
            jump = false;
        }

        if(sneak)
        {
            StartCoroutine(invisible());
        }

        if(sprinting)
        {
            Debug.Log("Sprinting");
            StartCoroutine(sprintTimer());
            
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void ReceiveInput(Vector2 _horizontalInput)
    {
        horizontalInput = _horizontalInput;
    }

    public void OnJumpPressed()
    {
        jump = true;
    }

    public void OnSneakPressed()
    {
        sneak = true;
    }

    public IEnumerator invisible()
    {
        for (int index = 0; index < 10; index++)
        {
            Hand.enabled = false;
            player.SetActive(false);
            yield return new WaitForSeconds(.1f);
        }
        Hand.enabled = true;
        player.SetActive(false);
        sneak = false;
    }

    public IEnumerator sprintTimer()
    {
        for (int index = 0; index < 12; index++)
        {
            speed = 12f;
            yield return new WaitForSeconds(.1f);
        }
        speed = 4f;
        sprinting = false;
    }

    public void OnSprintPressed()
    {
        sprinting = true;
    }
}

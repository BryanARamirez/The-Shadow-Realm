using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    public float speed = 10f;
    private Vector2 horizontalInput;

    [SerializeField] private float jumpHeight = 5f;
    private bool jump;

    [SerializeField] private float gravity = -1f;
    private Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    public bool sneak;
    [SerializeField] private MeshRenderer Hand;
    [SerializeField] private GameObject player;
    [SerializeField] private Text sneakChargeText;

    private float lastTime;
    private bool sneakReady;
    public int sneakCharge;
    public int sneakMax = 1;
    [SerializeField] private Image sneakActive;
    [SerializeField] private GameObject ammoFullTextGO;
    [SerializeField] private Text ammoFullText;


    private void Awake()
    {
        lastTime = Time.time;
        StartCoroutine(sneakTimer());
        sneakCharge = 1;
        sneakChargeText.text = "Sneak Charges: " + sneakCharge.ToString() + "/1";
    }

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

        if(sneak && (Time.time - lastTime > 5f))
        {
            StartCoroutine(invisible());
        }

        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);

        if(sneakReady)
        {
            sneakActive.enabled = true;
        }
        if (sneakReady == false)
        {
            sneakActive.enabled = false;
        }
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
        if (sneakCharge > 0)
        {
            if((Time.time - lastTime > 5f) && sneakReady == true)
            {
                sneak = true;
                sneakReady = false;
                sneakCharge--;
                sneakChargeText.text = "Sneak Charges: " + sneakCharge.ToString() + "/1";
                StartCoroutine(sneakTimer());
            }
        }
        else
        {
            // Debug Log states "You are out of Sneak Charges!"
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sneak Charge")
        {
            if (sneakCharge < sneakMax)
            {
                sneakCharge++;
                sneakChargeText.text = "Sneak Charges: " + sneakCharge.ToString() + "/1";
                Destroy(other.gameObject);
            }
            else
            {
                ammoFullText.text = "Sneak Charges Full";
                ammoFullTextGO.SetActive(true);
                Invoke("ammoFullTextOn", 2f);
            }
        }
    }

    public IEnumerator invisible()
    {
        for (int index = 0; index < 1; index++)
        {
            Hand.enabled = false;
            player.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
        Hand.enabled = true;
        player.SetActive(false);
        sneak = false;
        lastTime = Time.time;
    }
    private IEnumerator sneakTimer()
    {
        Debug.Log("Sneak Timer Started");
        for (int index = 0; index < 1f; index++)
        {
            yield return new WaitForSeconds(5f);
        }
        sneakReady = true;
    }
    private void ammoFullTextOn()
    {
        ammoFullTextGO.SetActive(false);
    }
}

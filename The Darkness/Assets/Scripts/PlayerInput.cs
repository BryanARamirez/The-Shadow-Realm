using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private MouseLook mouseLook;

    private PlayerControls controls;
    private PlayerControls.GroundMovementActions groundMovement;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;

    private float playerHealth = 15;


    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject spawnPointGO;
    private float enemyDamage;
    private bool takingDamage;
    private int lives = 3;
    public Text healthText;
    public Text livesText;
    public Text controlsText;
    public Text countText;
    public Text loseText;
    private int count;
    [SerializeField] private MeshRenderer damageIndicator;
    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.GroundMovement;

        //Gets WASD controls for movement
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();

        //To know when space is pressed to jump
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();

        //Reads mouse input
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();

        groundMovement.Sneak.performed += _ => movement.OnSneakPressed();
        groundMovement.Sprint.performed += _ => movement.OnSprintPressed();
        damageIndicator.enabled = false;
        SetCountText();
    }
    private void Start()
    {
        count = 0;
        SetCountText();
    }
    //When players is spawned in enable the controls 
    private void OnEnable()
    {
        controls.Enable();
    }
    //When player is destroyed disable controls to stop glitches and unintended actions
    private void OnDestroy()
    {
        controls.Disable();
    }

    private void Update()
    {
        //Constantly recieve the movement in the horizontal axis to update player location and movement
        movement.ReceiveInput(horizontalInput);
        //Recieve mouse information to put it into use
        mouseLook.ReceiveInput(mouseInput);
        if(takingDamage)
        {
            StartCoroutine(damageIndicatorShowing());
        }
        if (playerHealth <= 0)
        {
            respawn();
        }
        if (lives <= 0)
        {
            gameOver();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            takingDamage = true;
            enemyDamage = 5;
            takeDamage();
        }
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Respawn Point"))
        {
            spawnPointGO.transform.position = other.transform.position;
        }

    }

    public IEnumerator damageIndicatorShowing()
    {
        for (int index = 0; index < 20; index++)
        {
            damageIndicator.enabled = true;
            yield return new WaitForSeconds(.1f);
        }
        takingDamage = false;
        damageIndicator.enabled = false;


    }
    private void SetCountText()
    {
        healthText.text = "Health:" + playerHealth.ToString();
        livesText.text = "Lives: " + lives.ToString();
        countText.text = "Count:" + count.ToString();
    }
    private void takeDamage()
    {
        playerHealth = playerHealth - enemyDamage;
        SetCountText();
    }
    private void loseLife()
    {
        lives--;
        playerHealth = 15;
        SetCountText();
    }
    private void respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.position;
        damageIndicator.enabled = false;
        loseLife();
        GetComponent<CharacterController>().enabled = true;
    }
    private void gameOver()
    {
        loseText.text = "GameOver";
        damageIndicator.enabled = false;
        GetComponent<CharacterController>().enabled = false;
    }
}

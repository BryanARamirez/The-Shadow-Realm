using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Movement movement;
    [SerializeField] private StunGun stunGun;
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
    public int lives = 3;
    public Text healthText;
    public Text livesText;
    public Text countText;
    public Text loseText;
    private int keyCount;
    [SerializeField] private MeshRenderer damageIndicator;
    public Image detectedSense;
    [SerializeField] private PauseMenu pauseMenu;
    private float maxFallDistance = 100f;
    [SerializeField] private GameObject GameOverCanvas;

    private void Awake()
    {
        detectedSense.enabled = false;
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
        groundMovement.StunTaser.performed += _ => stunGun.OnStunPressed();
        damageIndicator.enabled = false;
        SetCountText();
    }
    private void Start()
    {
        keyCount = 0;
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
        if (transform.position.y < -maxFallDistance)
        {
            respawn();
        }
        if(pauseMenu.isPaused == true)
        {
            controls.Disable();
        }
        else
        {
            controls.Enable();
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
            keyCount = keyCount + 1;
            SetCountText();
        }
        if (other.tag == "Door")
        {
            if (keyCount >= other.gameObject.GetComponent<Door>().number_of_locks)
            {
                other.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Not enough Keys!");
            }
        }
        if (other.gameObject.CompareTag("Respawn Point"))
        {
            spawnPointGO.transform.position = other.transform.position;
        }
        if(other.tag == "slowMonster")
        {
            takingDamage = true;
            enemyDamage = 15;
            takeDamage();
        }
        if(other.tag == "Elevator")
        {
            Debug.Log("Elevator");
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
        countText.text = "Keys Held: " + keyCount.ToString();
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
    public void respawn()
    {
        GetComponent<CharacterController>().enabled = false;
        transform.position = spawnPoint.position;
        damageIndicator.enabled = false;
        loseLife();
        GetComponent<CharacterController>().enabled = true;
    }
    private void gameOver()
    {
        GameOverCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}

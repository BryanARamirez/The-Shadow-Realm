using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private PlayerInput playerInput;
    private InputAction menu;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject abilitiesMenu;
    [SerializeField] private GameObject controlsMenuCanvas;
    public bool isPaused;
    public bool controlsUp;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        menu = playerControls.Menu.Pause;
        menu.Enable();

        menu.performed += Pause;
    }

    private void OnDisable()
    {
        menu.Disable();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            EnableMenu();
        }
        else
        {
            /*if(controlsUp == false)
            {
                DisableMenu();
            }*/
            DisableMenu();
        }
    }

    private void EnableMenu()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        playerInput.enabled = false;
        pauseMenu.SetActive(true);
    }

    public void DisableMenu()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        controlsMenuCanvas.SetActive(false);
        controlsMenu.SetActive(true);
        abilitiesMenu.SetActive(false);
        playerInput.enabled = true;
        isPaused = false;
    }
}

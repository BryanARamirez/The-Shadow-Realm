using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private PlayerControls playerControls;
    private InputAction menu;

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private bool isPaused;

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
            DisableMenu();
        }
    }

    private void EnableMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    private void DisableMenu()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        isPaused = false;
    }
}

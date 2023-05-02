using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    [SerializeField] private GameObject backMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject backToControls;
    [SerializeField] private GameObject abilitiesMenu;
    [SerializeField] private PauseMenu playerContainer;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void checkControls()
    {
        controlsMenu.SetActive(true);
        backMenu.SetActive(false);
        playerContainer.controlsUp = true;
    }
    public void Back()
    {
        controlsMenu.SetActive(false);
        backMenu.SetActive(true);
        playerContainer.controlsUp = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void CheckAbilities()
    {
        abilitiesMenu.SetActive(true);
        backToControls.SetActive(false);
    }
    public void CloseAbilities()
    {
        abilitiesMenu.SetActive(false);
        backToControls.SetActive(true);
    }
}

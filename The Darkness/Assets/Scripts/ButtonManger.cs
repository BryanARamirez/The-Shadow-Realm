using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManger : MonoBehaviour
{
    [SerializeField] private GameObject backMenu;
    [SerializeField] private GameObject controlsMenu;

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
    }
    public void Back()
    {
        controlsMenu.SetActive(false);
        backMenu.SetActive(true);
    }
}

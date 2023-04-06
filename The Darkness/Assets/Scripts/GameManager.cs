using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private PlayerInput playerInputScript;
    [SerializeField] private Image restartGameButton;
    [SerializeField] private Text restartGameText;

    private void Awake()
    {
        isGameOver = false;
        restartGameButton.enabled = false;
        restartGameText.enabled = false;
    }

    private void Update()
    {
        if (isGameOver == true)
        {
            playerInputScript.enabled = false;
            restartGameButton.enabled = true;
            restartGameText.enabled = true;
        }
        if (playerInputScript.lives <= 0)
        {
            isGameOver = true;
        }
        if(isGameOver == true && Input.GetKey("t"))
        {
            Scene scene = SceneManager.GetActiveScene(); 
            SceneManager.LoadScene(scene.name);
        }
    }
}

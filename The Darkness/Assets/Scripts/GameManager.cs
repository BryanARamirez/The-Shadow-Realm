using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isGameOver;
    [SerializeField] private PlayerInput playerInputScript;

    private void Awake()
    {
        isGameOver = false;
    }

    private void Update()
    {
        if (isGameOver == true)
        {
            playerInputScript.enabled = false;
        }
        if (playerInputScript.lives <= 0)
        {
            isGameOver = true;
        }
    }
}

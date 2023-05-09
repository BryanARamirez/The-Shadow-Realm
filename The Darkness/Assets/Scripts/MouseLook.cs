using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float sensX = 50f;
    public float sensY = 0.5f;
    private float mouseX, mouseY;
    [SerializeField] PauseMenu pauseMenuScript;

    [SerializeField] private Slider slider;

    [SerializeField] private Transform playerCamera;
    [SerializeField] private float xClamp = 85f;
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        slider.value = 25;
    }

    private void Update()
    {
        sensX = slider.value / 1.5f;
        sensY = slider.value / 600;
        //transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
        //Helps clamp the camera so you can't look all the way behind yourself
        if(pauseMenuScript.isPaused == false)
        {
            transform.Rotate(Vector3.up, mouseX * Time.deltaTime);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
            Vector3 targetRotation = transform.eulerAngles;
            targetRotation.x = xRotation;
            playerCamera.eulerAngles = targetRotation;
        }
        /*xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp);
        Vector3 targetRotation = transform.eulerAngles;
        targetRotation.x = xRotation;
        playerCamera.eulerAngles = targetRotation;*/
    }

    public void ReceiveInput(Vector2 mouseInput)
    {
        mouseX = mouseInput.x * sensX;
        mouseY = mouseInput.y * sensY;
    }
}

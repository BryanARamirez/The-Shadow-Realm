using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprintMeter : MonoBehaviour
{
    private float maxStamina = 100f;
    private float sprintSpeed = 20f;
    private float regularSpeed = 10f;
    private float staminaDrainRate = 20f;
    [SerializeField] private Image staminaBar;
    private bool isTired = false;
    private bool isSprinting = false;
    private float currentStamina;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (isSprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina <= 0f)
            {
                isSprinting = false;
                StartCoroutine(StaminaRefillTimer());
                GetComponent<Movement>().speed = regularSpeed;
            }
        }
        else
        {
            if(isTired == false)
            {
                currentStamina += staminaDrainRate * Time.deltaTime;
                currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            }

            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }

        if(Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f)
        {
            isSprinting = true;
            GetComponent<Movement>().speed = sprintSpeed;
        }
        else
        {
            isSprinting = false;
            GetComponent<Movement>().speed = regularSpeed;
        }
        staminaBar.fillAmount = currentStamina / maxStamina;
    }

    private IEnumerator StaminaRefillTimer()
    {
        for (int index = 0; index < 1f; index++)
        {
            isTired = true;
            yield return new WaitForSeconds(4f);
        }
        isTired = false;
    }
}

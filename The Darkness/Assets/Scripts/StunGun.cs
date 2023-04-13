using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class StunGun : MonoBehaviour
{
    [SerializeField] private GameObject stunTaser;
    private int stunAmmo;
    [SerializeField] private Text stunAmmoText;
    private bool isInUse = false;

    private void Awake()
    {
        stunAmmo = 3;
        stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnStunPressed()
    {
        if(stunAmmo > 0 && isInUse == false)
        {
            StartCoroutine(stunTimer());
        }
        
    }
    private IEnumerator stunTimer()
    {
        isInUse = true;
        for (int index = 0; index < 1f; index++)
        {
            stunTaser.SetActive(true);
            yield return new WaitForSeconds(2f);
        }
        stunAmmo--;
        stunTaser.SetActive(false);
        isInUse = false;
        stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString();
    }
}

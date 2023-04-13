using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class StunGun : MonoBehaviour
{
    [SerializeField] private GameObject stunTaser;
    public int stunAmmo;
    [SerializeField] private Text stunAmmoText;
    private bool isInUse = false;

    private void Awake()
    {
        stunAmmo = 3;
        stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StunAmmo")
        {
            if(stunAmmo < 3)
            {
                stunAmmo++;
                stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString();
                Destroy(other.gameObject);
            }
        }
    }
}

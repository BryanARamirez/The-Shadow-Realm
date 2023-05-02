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
    [SerializeField] private GameObject ammoFullTextGO;
    [SerializeField] private Text ammoFullText;

    private void Awake()
    {
        stunAmmo = 3;
        stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString() + "/3";
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
            stunAmmo--;
            stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString() + "/3";
            yield return new WaitForSeconds(3f);
        }
        stunTaser.SetActive(false);
        isInUse = false;
        stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString() + "/3";
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "StunAmmo")
        {
            if(stunAmmo < 3)
            {
                stunAmmo++;
                stunAmmoText.text = "Stun Ammo: " + stunAmmo.ToString() + "/3";
                Destroy(other.gameObject);
            }
            else
            {
                ammoFullText.text = "Stun Ammo Full";
                ammoFullTextGO.SetActive(true);
                Invoke("ammoFullTextOn", 2f);
            }
        }
    }

    private void ammoFullTextOn()
    {
        ammoFullTextGO.SetActive(false);
    }
}

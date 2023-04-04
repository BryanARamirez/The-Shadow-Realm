using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    [SerializeField] private Material respawnPointActive;
    //in the editor this is what you would set as the object you wan't to change
    [SerializeField]private GameObject Object;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            changeToActive();
        }
    }

    private void changeToActive()
    {
        Object.GetComponent<MeshRenderer>().material = respawnPointActive;
    }
}

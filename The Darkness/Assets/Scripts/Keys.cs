using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour
{
    public int keys = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Key")
        {
            keys++;
            print("picked up a key");
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.tag == "Door")
        {
            Debug.Log("ran into door");
            if (keys >= other.gameObject.GetComponent<Door>().number_of_locks)
            {
                keys -= other.gameObject.GetComponent<Door>().number_of_locks;
                other.gameObject.SetActive(false);
            }
        }
    }
}

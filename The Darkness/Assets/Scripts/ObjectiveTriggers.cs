using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveTriggers : MonoBehaviour
{
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private GameObject levelEnd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respawn Point"))
        {
            checkpoint.SetActive(true);
            Invoke("checkpointText", 2f);
        }
        if (other.gameObject.CompareTag("Level End"))
        {
            levelEnd.SetActive(true);
            Invoke("levelEndText", 2f);
            Destroy(other);
        }
    }

    private void checkpointText()
    {
        checkpoint.SetActive(false);
    }
    private void levelEndText()
    {
        levelEnd.SetActive(false);
    }
}

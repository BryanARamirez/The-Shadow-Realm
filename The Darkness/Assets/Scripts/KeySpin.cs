using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpin : MonoBehaviour
{
    public float speed;
    private void Update()
    {
        transform.Rotate(new Vector3(Time.deltaTime * speed * 10, 0, 0));
    }
}

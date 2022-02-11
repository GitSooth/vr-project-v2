using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float speed = 1f;
    void Update()
    {
        transform.Rotate(0, speed, 0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotateZ = 1f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateZ);    
    }
}

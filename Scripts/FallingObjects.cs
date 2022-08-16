using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    // Update is called once per frame
    bool falling = false;
    void Update()
    {
        if(transform.position.x >= -7f)
        {
            falling = true;
        }   
    }

    public bool fallingActivate()
    {
        if (falling) return true;
        else return false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandHitBox : MonoBehaviour
{

    public event Action playerHit;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerHit.Invoke();
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{

    public event Action playerAttackAble; 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerAttackAble.Invoke();
        }
    }
}

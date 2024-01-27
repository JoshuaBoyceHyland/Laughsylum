using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class WhoopieCushionPickUp : MonoBehaviour
{
    bool canBePickedUp = false;
    bool isPickedUp = false;

    public GameObject pickupText;

    private void Update()
    {

        if (canBePickedUp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Cushion Picked up");
                isPickedUp = true;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

         canBePickedUp = true;

    

    }
    private void OnTriggerExit(Collider other)
    {
        canBePickedUp = false; 
    }

}

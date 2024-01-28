using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
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
                Debug.Log("Picked up");
                isPickedUp = true;
                Destroy(gameObject);

                if (gameObject.tag == "Key")
                {
                    GameObject.Find("Player").GetComponent<PlayerController>().hasKey = true;
                    Debug.Log("Has a key");
                }
            }
            else
            {
                isPickedUp=false;
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

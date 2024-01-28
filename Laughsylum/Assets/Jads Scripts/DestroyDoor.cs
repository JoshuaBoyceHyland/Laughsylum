using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoor : MonoBehaviour
{

    public PickUp playerPickup;
    public bool canDooropen = false;
    bool playerNearDoor = false;
    public bool playerhasWon = false; 

    public PlayerController playerController;   
    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                canDooropen = true;
                Destroy(gameObject);
                playerhasWon = true;
                Debug.Log("Door can open");
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {



                playerNearDoor = true; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerNearDoor = false;
    }

}

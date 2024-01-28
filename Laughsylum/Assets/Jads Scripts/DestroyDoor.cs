using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
         
                playerhasWon = true;
                Debug.Log("Door can open");
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {



            playerNearDoor = true;
        }
    }


    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNearDoor = false;
        }
    }
    

   

}

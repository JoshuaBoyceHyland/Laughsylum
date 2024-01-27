using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;


enum States { Idle = 0 ,Walking =1, Death =2}   
public class PlayerController : MonoBehaviour
{

    public float y, x;
    private Rigidbody rb;

    public bool grounded;
    
    public float walkSpeed = 5f, sensitivity = 2f;

    public AudioSource footsteps;
    

    [SerializeField] Animator animator;
    States state;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Idle;
        animator.SetInteger("State", (int)state); 
        Cursor.lockState = CursorLockMode.Locked;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetInteger("State", (int)state);
        grounded = Physics.Raycast(rb.transform.position, Vector3.down, Camera.main.transform.localPosition.y + 1f);
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }

        Look();
    }

    void Look()
    {
        x -= Input.GetAxisRaw("Mouse Y") * sensitivity;
        x = Mathf.Clamp(x, -90, 90);
        y += Input.GetAxisRaw("Mouse X") * sensitivity;
        Camera.main.transform.localRotation = Quaternion.Euler(x, y, 0);
       
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (rb.velocity == Vector3.zero)
        {
            state = States.Idle;
            Debug.Log("IDle");
            footsteps.enabled = false;
        }
        else
        {
            state = States.Walking;
            footsteps.enabled = true;
        }
        Vector2 axis = new Vector2(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")).normalized * walkSpeed;
        Vector3 forward = new Vector3(-Camera.main.transform.right.z, 0, Camera.main.transform.right.x);
        Vector3 moveDirection = (forward * axis.x + Camera.main.transform.right * axis.y + Vector3.up * rb.velocity.y);
        rb.velocity = moveDirection;

       
        
    }
}

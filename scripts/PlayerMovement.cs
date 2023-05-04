using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 5f;
    
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;
    
    [SerializeField] AudioSource jumpSound;

    public static int rotationState;

    // Start is called before the first frame update
    void Start()
    {
        rotationState = 1;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        rb.velocity = new Vector3(horizontalInput*movementSpeed, rb.velocity.y,verticalInput*movementSpeed);

        switch(rotationState) 
        {
          case 1:
            rb.velocity = new Vector3(horizontalInput*movementSpeed, rb.velocity.y,verticalInput*movementSpeed);
            break;
          case 2:
            rb.velocity = new Vector3(-verticalInput*movementSpeed, rb.velocity.y,horizontalInput*movementSpeed);
            break;
          case 3:
            rb.velocity = new Vector3(-horizontalInput*movementSpeed, rb.velocity.y,-verticalInput*movementSpeed);
            break;
          case 4:
            rb.velocity = new Vector3(verticalInput*movementSpeed, rb.velocity.y, -horizontalInput*movementSpeed);
            break;

          default:
            rb.velocity = new Vector3(horizontalInput*movementSpeed, rb.velocity.y,verticalInput*movementSpeed);
            break;
        }
        
        if(Input.GetKeyDown("space") && IsGrounded())
        {
            Jump();
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        jumpSound.Play();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Spot"))
        {
            Destroy(collision.transform.parent.gameObject);
            Jump();
        }    
    }
    
    bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
}

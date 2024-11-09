using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movespeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public Transform orientation;

    Vector3 moveDirection;

    private Vector2 moveInput;

   // private Vector2 lookInput;
    [SerializeField] InputActionPlayer playerControls;

    [SerializeField] Rigidbody rb;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    private void Awake()
    {
        playerControls = new InputActionPlayer();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    private void OnEnable()
    {
      playerControls.Enable();

    }

    private void OnDisable()
    {
       playerControls.Disable();

    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        if(grounded)
        rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);


        else if(!grounded)
            rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else rb.drag = 0;
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);

        //limnit przyspieszenia
        if(flatVel.magnitude > movespeed)
        {
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(limitedVel.x,rb.velocity.y,limitedVel.z);
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {   
        if(context.performed && grounded && readyToJump)
        {   
            readyToJump = false;
            Debug.Log("elo");
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);

        }

        if(context.performed && readyToJump && grounded)
        {

            readyToJump = false;
            //Jump();
            //reset przyspieszenie Y
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void Jump()
    {

    }

    private void ResetJump()
    {
        readyToJump = true;
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }
}

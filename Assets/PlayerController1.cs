using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;


using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerController1 : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movespeed;
   

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public bool wallCoolDown;
    public bool readyToJump;

 
    public GameObject wallPrefab; // Prefab �ciany
    public Transform playerCamera; // Kamera gracza (lub gracz)
    public float spawnDistance = 3f; // Odleg�o��, na kt�rej �ciana zostanie postawiona

    public int maxWalls = 4; // Maksymalna liczba �cian
    private int wallsPlaced = 0; // Licznik postawionych �cian





    public Transform orientation;

    Vector3 moveDirection;

    private Vector2 moveInput;

   // private Vector2 lookInput;
    [SerializeField] PlayerInput playerControls;

    [SerializeField] Rigidbody rb;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public bool grounded;

    private void Awake()
    {
        playerControls = new PlayerInput();

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
        wallCoolDown = true;
    }

    private void OnEnable()
    {
      playerControls.Enable();
      playerControls.Player.SpawnWall.performed += SpawnWall; 

    }

    private void OnDisable()
    {
       
       playerControls.Player.SpawnWall.performed -= SpawnWall; 
       playerControls.Disable();

    }

  
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * moveInput.y + orientation.right * moveInput.x;
        if (grounded)
        {   if (moveDirection.magnitude > 0)
            {
                rb.velocity = new Vector3(moveDirection.normalized.x * movespeed, rb.velocity.y, moveDirection.normalized.z * movespeed);
            }
            else
            {
                /*rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);*/
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }


        else if (!grounded)
            rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f, whatIsGround);

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

    private void SpawnWall(InputAction.CallbackContext context)
    {
        if (context.performed && wallCoolDown)
        {
            
            if (wallsPlaced >= maxWalls)
            {
                Debug.Log("Nie mo�esz postawi� wi�cej �cian.");
                return;
            }
            

            wallCoolDown = false;   

            if (wallPrefab == null || playerCamera == null)
            {
                Debug.LogWarning("Nie mo�na postawi� �ciany.Upewnij si�, �e wallPrefab oraz playerCamera s� przypisane.");
                return;
            }

            // Obliczenie pozycji przed graczem
            Vector3 spawnPosition = playerCamera.position + playerCamera.forward * spawnDistance;


            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.5f, whatIsGround);

            // Wyr�wnanie pozycji do pod�o�a za pomoc� Raycast
            if (Physics.Raycast(spawnPosition + Vector3.up, Vector3.down, out RaycastHit hitInfo))
            {
                spawnPosition = hitInfo.point; // Przesuni�cie na powierzchni� pod�o�a
            }

            // Obr�t: ignorujemy nachylenie kamery w osi Y
            Vector3 forwardDirection = playerCamera.forward;
            forwardDirection.y = 0; // Ustawienie p�askiej orientacji
            Quaternion spawnRotation = Quaternion.LookRotation(forwardDirection);

            // Spawnowanie �ciany
            Instantiate(wallPrefab, spawnPosition, spawnRotation);

            Invoke(nameof(resetWallsCoolDown), .5f);

            // Zwi�kszenie licznika
            wallsPlaced++;
            
        }
    }
    

/*    public void onJump(InputAction.CallbackContext context)

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
    }*/

    public void onShoot(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Debug.Log("Strzal");
         

        }
    }

    private void Jump()
    {

    }

    private void ResetJump()
    {
        readyToJump = true;
    }
    
    private void resetWallsCoolDown()
    {
        wallCoolDown = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * (playerHeight * 0.5f));
    }

}

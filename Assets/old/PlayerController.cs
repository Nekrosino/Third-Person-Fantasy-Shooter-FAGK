using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float rotationspeed = 1f;
    private Vector2 moveInput;
    private Vector2 lookInput;
    [SerializeField] InputActionPlayer playerControls;
    [SerializeField] Rigidbody rb;

    private void Awake()
    {
        playerControls = new InputActionPlayer();
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

    public void onLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * speed;
        rb.velocity = move;
        rb.transform.Rotate(Vector3.up, lookInput.x * rotationspeed);
    }

}

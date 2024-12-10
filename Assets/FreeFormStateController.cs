using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeFormStateController : MonoBehaviour
{
    Animator animator;
    float velocityX = 0.0f;
    float velocityZ = 0.0f;
    public float acceleration = 0.2f;
    public float deceleration = 0.5f;
    bool isJumping;
    int VelocityHashX;
    int VelocityHashZ;
    int isJumpingHash;
    void Start()
    {
        animator = GetComponent<Animator>();
        VelocityHashX = Animator.StringToHash("VelocityX");
        VelocityHashZ = Animator.StringToHash("VelocityZ");
        isJumpingHash = Animator.StringToHash("isJumping");
        isJumping = false;
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool backwardPressed = Input.GetKey(KeyCode.S);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKey(KeyCode.Space);

        //Klawisz W lub S poruszaj do przodu
        if (forwardPressed && velocityZ < 1.0f)
        {
            velocityZ += Time.deltaTime * acceleration;
        }
        //Klawisz W lub S puszczony - zatrzymuj postaæ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }
        //Kontrola minimalnej predkosci zatrzymania (nie moze byc nizej niz 0)
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }
        //Kontrola maksymalnej predkosci (nie moze byc wieksza niz 1)
        if (forwardPressed && velocityZ > 1.0f)
        {
            velocityZ = 1.0f;
        }
        if (leftPressed && velocityX > -1f)
        {
            velocityX -= Time.deltaTime * acceleration;
        }
        if (!leftPressed && velocityX < 0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }
        if (rightPressed && velocityX < 1f)
        {
            velocityX += Time.deltaTime * acceleration;
        }
        if (!rightPressed && velocityX > 0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }
        if (rightPressed && velocityX > 1.0f)
        {
            velocityX = 1.0f;
        }
        if (leftPressed && velocityX < -1f)
        {
            velocityX = -1f;
        }
        if(jumpPressed && !isJumping)
        {
            animator.SetBool(isJumpingHash, true);
            Invoke(nameof(onJump), 2f);
        }


        animator.SetFloat(VelocityHashZ, velocityZ);
        animator.SetFloat(VelocityHashX, velocityX);


    }

    public void onJump()
    {
        animator.SetBool(isJumpingHash, false);
    }

    public void resetJump()
    {
        isJumping = false;
    }

}

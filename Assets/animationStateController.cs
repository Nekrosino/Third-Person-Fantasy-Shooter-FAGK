using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    float velocity = 0.0f;
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool forwardPressed = Input.GetKey(KeyCode.UpArrow);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }

        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningHash, true);
        }

        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningHash, false);
        }
    }


    /*    Animator animator;
        float velocity = 0.0f;
        public float acceleration = 0.2f;
        public float deceleration = 0.5f;
        int VelocityHash;
       void Start()
        {
            animator = GetComponent<Animator>();
            VelocityHash = Animator.StringToHash("Velocity");
        }

        void Update()
        {
            bool forwardPressed = Input.GetKey(KeyCode.UpArrow);
            bool runPressed = Input.GetKey(KeyCode.LeftShift);

            if(forwardPressed && velocity < 1.0f)
            {
                velocity += Time.deltaTime * acceleration;
            }

            if(!forwardPressed && velocity > 0.0f)
            {
                velocity -= Time.deltaTime * deceleration;
            }

            if(!forwardPressed && velocity < 0.0f)
            {
                velocity = 0.0f;
            }
            animator.SetFloat(VelocityHash, velocity);


        }*/
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestCharController : MonoBehaviour
{

    public float movementSpeed = 10f;
    public SpawnManager spawnManager;

    public float rotationSpeed;
    public float jumpSpeed;
    public CharacterController characterController;
    private float ySpeed;
    public Animator animator = new Animator();
    //    public Rigidbody myRigidbody;
    private bool isJump = false;
    private bool run = false;

    // Start is called before the first frame update
    void Start()
    {
        // characterController= GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        float hMovement = Input.GetAxis("Horizontal") * movementSpeed / 2;
        // forward
        float vMovement = Input.GetAxis("Vertical") * movementSpeed;
        Vector3 movementDirection = new Vector3(hMovement, 0, vMovement);
        Vector3 velocity = movementDirection;
        ySpeed += Physics.gravity.y * Time.deltaTime;

        // run forward

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            if (!isJump)
            {
                ResetAllBool();
                animator.SetBool("Run Forward", true);
            }
            run = true;

        }
        StopAnimation(movementDirection);

        if (characterController.isGrounded)
        {
            // resotre movement and jump status after hit the ground
            if (isJump)
            {
                if (run == true)
                {
                    animator.SetBool("Run Forward", true);
                }
                isJump = false;
            }
            else
            {
                // if jump buttom is pressed
                ySpeed = -0.5f;
                if (Input.GetButtonDown("Jump"))
                {
                    // myRigidbody.velocity = Vector3.up * 7;
                    ySpeed = jumpSpeed;
                    ResetAllBool();
                    animator.SetBool("Jump", true);
                    isJump = true;
                }
            }

        }

        //if (hMovement > 0 || vMovement > 0)
        //{
        //    animationTest.animator.SetBool("Run Forward", true);
        //} else
        //{
        //    animationTest.ResetAllBool();
        //    animationTest.animator.SetBool("Idle", true);
        //}

        // transform.Translate(new Vector3(hMovement, 0, vMovement) * Time.deltaTime);
        // https://www.youtube.com/watch?v=BJzYGsMcy8Q

        // transform.Translate(velocity);
        velocity.y = ySpeed;
        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            // transform.forward = movementDirection;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnTriggerEntered();
    }

    // Char Animation
    public void StopAnimation(Vector3 movement)
    {
        if (movement == Vector3.zero && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)
            && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            if (!isJump)
            {
                ResetAllBool();
                animator.SetBool("Idle", true);
            }
            run = false;
        }
    }
    public void ResetAllBool()
    {
        animator.SetBool("Run Forward", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Jump", false);
    }
}

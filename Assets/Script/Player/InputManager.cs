using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputManager : MonoBehaviour
{
    AnimationManager animationManager;

    Vector3 velocity;
    Animator animator;
    CharacterController controller;
    public float speed;
    public float turnSmoothTime;
    float turnSmoothVelocity;
    protected FloatingJoystick joystick;
    FloatingJoystick movement;
    public float ySpeed;
    public Transform cam;
    float tunrSmoothVelocity;
    public ParticleSystem skiL;
    public ParticleSystem skiR;
    Vector3 moveDir;
    public static bool grounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        joystick = FindObjectOfType<FloatingJoystick>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref tunrSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        moveDir.y = ySpeed * Time.deltaTime;
        if (controller.isGrounded)
        {
            ySpeed = -0.5f;
        }
        else
        {
            ySpeed = -200f;
            controller.Move(moveDir * Time.deltaTime);
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Ski")
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Ski", true);
            skiL.Play();
            skiR.Play();
        }
        else
        {
            animator.SetBool("Ski", false);
            skiL.Stop();
            skiR.Stop();
        }
        if (hit.gameObject.tag == "IsGrounded")
        {
            grounded = true;
        }
        else
        {
            if (hit.gameObject.tag == "Ski" || hit.gameObject.tag == "Stairs")
            {
                grounded = false;
            }
        }
    }
}

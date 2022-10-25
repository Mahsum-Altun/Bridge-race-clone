using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    Transform target;
    InputManager ınputManager;
    GameObject brickTransformParent;
    GameObject player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        target = GetComponent<Transform>();
        ınputManager = FindObjectOfType<InputManager>();
        characterController = GetComponent<CharacterController>();
        player = GameObject.Find("Player");
        brickTransformParent = player.transform.GetChild(3).gameObject;
    }

    private void Update()
    {

        if (target.hasChanged)
        {
            animator.SetBool("Walk", true);
            target.hasChanged = false;
            if (brickTransformParent.transform.childCount > 0)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", true);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }

    public void FallOpen()
    {
        ınputManager.speed = 0f;
    }
    public void IdleOpen()
    {
        ınputManager.speed = 4f;
    }
}

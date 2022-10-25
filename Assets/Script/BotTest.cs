using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BotTest : MonoBehaviour
{
    public GameObject windesk1;
    public GameObject windesk2;
    public GameObject windesk3;
    public GameObject blueNPC;
    public GameObject whiteNPC;
    public NavMeshAgent blueAgent;
    public NavMeshAgent whiteAgent;
    public NavMeshAgent yellowAgent;
    public GameObject playerJoystictk;
    public GameObject brickTransformParent;
    public GameObject yellowTransformParent;
    public GameObject blueTransformParent;
    public GameObject whiteTransformParent;
    public float fallSpeed;
    Vector3 velocity;
    CharacterController characterController;
    Animator animator;
    public Animator blueAnimator;
    public Animator whiteAnimator;
    public Animator yellowAnimator;
    private InputManager inputManager;
    public GameObject winPanel;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public GameObject cameraEffect;
    public AudioClip winSound;
    public AudioClip fallSound;
    public AudioSource audioSource;
    public AudioClip skiSound;
    private Collider fallCol;
    AdsManager adsManager;

    private void Start()
    {
        characterController = GetComponentInParent<CharacterController>();
        animator = GetComponentInParent<Animator>();
        inputManager = FindObjectOfType<InputManager>();
        fallCol = GetComponent<Collider>();
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (InputManager.grounded == true)
        {
            if (other.gameObject.tag == "Enemy Blue" || other.gameObject.tag == "Enemy White" || other.gameObject.tag == "Enemy Yellow")
            {
                if (brickTransformParent.transform.childCount < yellowTransformParent.transform.childCount || brickTransformParent.transform.childCount < whiteTransformParent.transform.childCount || brickTransformParent.transform.childCount < blueTransformParent.transform.childCount)
                {
                    velocity.y = fallSpeed;
                    velocity.z = fallSpeed;
                    characterController.Move(velocity * Time.deltaTime);
                    inputManager.speed = 0f;
                    animator.SetTrigger("Fall");
                    StartCoroutine("FallSound");
                    audioSource.PlayOneShot(fallSound);
                    fallCol.enabled = false;
                }
            }
        }
        if (other.gameObject.tag == "Win")
        {
            CameraFollow.win = true;
            StartCoroutine("Win");
            inputManager.speed = 0f;
            GetComponentInParent<InputManager>().enabled = false;
            transform.parent.position = windesk1.transform.position;
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y + 180, transform.parent.eulerAngles.z);
            playerJoystictk.gameObject.SetActive(false);
            animator.SetTrigger("Win");
            blueNPC.transform.position = windesk2.transform.position;
            blueNPC.transform.eulerAngles = new Vector3(blueNPC.transform.eulerAngles.x, blueNPC.transform.eulerAngles.y + 180, blueNPC.transform.eulerAngles.z);
            blueAgent.enabled = false;
            blueAnimator.SetTrigger("Lose");
            whiteNPC.transform.position = windesk3.transform.position;
            whiteNPC.transform.eulerAngles = new Vector3(whiteNPC.transform.eulerAngles.x, whiteNPC.transform.eulerAngles.y + 180, whiteNPC.transform.eulerAngles.z);
            whiteAgent.enabled = false;
            whiteAnimator.SetTrigger("Lose");
            yellowAgent.enabled = false;
            yellowAnimator.SetTrigger("Lose");
            audioSource.PlayOneShot(winSound);
            StartCoroutine("AdsControl");
        }
        if (other.gameObject.tag == "Ski")
        {
            audioSource.PlayOneShot(skiSound);
        }
        else
        {
            if (other.gameObject.tag == "IsGrounded")
            {
                audioSource.Stop();
            }
        }
    }
    IEnumerator Win()
    {
        yield return new WaitForSeconds(3f);
        winPanel.SetActive(true);
        cameraEffect.SetActive(true);
        yield return new WaitForSeconds(.5f);
        star1.SetActive(true);
        yield return new WaitForSeconds(.5f);
        star2.SetActive(true);
        yield return new WaitForSeconds(.5f);
        star3.SetActive(true);
    }
    IEnumerator FallSound()
    {
        yield return new WaitForSeconds(1f);
        fallCol.enabled = true;
    }
    IEnumerator AdsControl()
    {
        yield return new WaitForSeconds(1f);
        adsManager.interstitial();
    }

}
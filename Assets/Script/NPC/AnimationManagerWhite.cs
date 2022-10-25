using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationManagerWhite : MonoBehaviour
{
    Animator animator;
    public Animator yellowAnimator;
    public Animator blueAnimator;
    public GameObject brickTransformParent;
    public GameObject transformParent1;
    public GameObject transformParent2;
    public GameObject transformParent3;
    public float fallSpeed;
    Vector3 velocity;
    private NavMeshAgent agent;
    public NavMeshAgent yellowAgent;
    public NavMeshAgent blueAgent;
    public static bool die;
    Rigidbody rb;
    public bool grounded;
    public GameObject windesk1;
    public GameObject windesk2;
    public GameObject windesk3;
    public GameObject yellowNPC;
    public GameObject blueNPC;
    public GameObject defeatPanel;
    public AudioSource audioSource;
    AdsManager adsManager;

    void Start()
    {
        animator = transform.parent.GetComponent<Animator>();
        agent = transform.parent.GetComponent<NavMeshAgent>();
        rb = transform.parent.GetComponent<Rigidbody>();
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grounded == true)
        {
            if (other.gameObject.tag == "Enemy Blue" || other.gameObject.tag == "Enemy Yellow" || other.gameObject.tag == "Player")
            {
                if (brickTransformParent.transform.childCount < transformParent1.transform.childCount || brickTransformParent.transform.childCount < transformParent2.transform.childCount || brickTransformParent.transform.childCount < transformParent3.transform.childCount)
                {
                    velocity.x = fallSpeed;
                    velocity.y = fallSpeed;
                    velocity.z = fallSpeed;
                    agent.Move(velocity * Time.deltaTime);
                    agent.speed = 0f;
                    animator.SetTrigger("Fall");
                }
            }
        }
        if (other.gameObject.tag == "Die Wall")
        {
            die = true;
            rb.isKinematic = false;
            agent.enabled = false;
            StartCoroutine("SetActiveEnemy");
        }
        if (other.gameObject.tag == "Win")
        {
            CameraFollow.win = true;
            StartCoroutine("Defeat");
            transform.parent.position = windesk1.transform.position;
            transform.parent.eulerAngles = new Vector3(transform.parent.eulerAngles.x, transform.parent.eulerAngles.y + 180, transform.parent.eulerAngles.z);
            agent.enabled = false;
            animator.SetTrigger("Win");
            yellowNPC.transform.position = windesk2.transform.position;
            yellowNPC.transform.eulerAngles = new Vector3(yellowNPC.transform.eulerAngles.x, yellowNPC.transform.eulerAngles.y + 180, yellowNPC.transform.eulerAngles.z);
            yellowAgent.enabled = false;
            yellowAnimator.SetTrigger("Lose");
            blueNPC.transform.position = windesk3.transform.position;
            blueNPC.transform.eulerAngles = new Vector3(blueNPC.transform.eulerAngles.x, blueNPC.transform.eulerAngles.y + 180, blueNPC.transform.eulerAngles.z);
            blueAgent.enabled = false;
            blueAnimator.SetTrigger("Lose");
            audioSource.Play();
            StartCoroutine("AdsControl");
        }
        if (other.gameObject.tag == "IsGrounded")
        {
            grounded = true;
        }
        else
        {
            if (other.gameObject.tag == "Ski" || other.gameObject.tag == "Stairs" || other.gameObject.tag == "Yellow Stairs" || other.gameObject.tag == "White Stairs" || other.gameObject.tag == "Blue Stairs" || other.gameObject.tag == "Red Stairs")
            {
                grounded = false;
            }
        }
    }
    IEnumerator SetActiveEnemy()
    {
        yield return new WaitForSeconds(4f);
        transform.parent.gameObject.SetActive(false);
    }
    IEnumerator Defeat()
    {
        yield return new WaitForSeconds(3f);
        defeatPanel.SetActive(true);
    }
      public void DieRestart()
    {
        die = false;
    }
    IEnumerator AdsControl()
    {
        yield return new WaitForSeconds(1f);
        adsManager.interstitial();
    }
}

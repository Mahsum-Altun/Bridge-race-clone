using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickTransport : MonoBehaviour
{

    GameObject brickTransform;
    GameObject brickTransformParent;
    public float brickSpeed;
    bool brickTouch = false;
    public float brickPosY;
    Vector3 newBrickTransform;
    public Transform Target;
    public float RotationSpeed;
    private Quaternion lookRotation;
    private Vector3 direction;
    public Rigidbody rb;
    public Collider col;
    Animator animator;
    public Material fallMat;
    public Material originalMat;
    GameObject player;
    GameObject redBrickSpawnParent;
    public ParticleSystem brickEffect1;
    public ParticleSystem brickEffect2;
    public AudioSource brickSound;
    private bool brickSoundClosed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        player = GameObject.Find("Player");
        brickTransform = player.transform.GetChild(4).gameObject;
        brickTransformParent = player.transform.GetChild(3).gameObject;
        animator = GameObject.Find("Player").GetComponent<Animator>();
        Target = player.transform.GetChild(4).transform;
        redBrickSpawnParent = GameObject.Find("Red BrickSpawn Parent");
        transform.parent = redBrickSpawnParent.transform;
    }

    private void Update()
    {

        direction = (Target.position - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        if (brickTouch == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, brickTransform.transform.position, Time.deltaTime * brickSpeed);
            transform.parent = brickTransformParent.transform;
            brickEffect1.Play();
            brickEffect2.Play();


            if (transform.position == brickTransform.transform.position)
            {
                col.isTrigger = false;
                brickTouch = false;
                newBrickTransform = new Vector3(brickTransform.transform.position.x, brickTransform.transform.position.y + brickPosY, brickTransform.transform.position.z);
                brickTransform.transform.position = newBrickTransform;
                brickEffect1.Stop();
                brickEffect2.Stop();
            }

        }
        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("Sweep Fall") == true)
        {
            brickTouch = false;
            if (col.isTrigger == false)
            {
                rb.isKinematic = false;
            }
            brickTransformParent.transform.DetachChildren();
            brickTransform.transform.localPosition = new Vector3(0, 1.4f, -0.3200004f);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            brickTouch = true;
            GetComponent<MeshRenderer>().material = originalMat;
            if (brickSoundClosed == false)
            {
                brickSound.Play();
                brickSoundClosed = true;
            }
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "IsGrounded")
        {
            GetComponent<MeshRenderer>().material = fallMat;
            rb.isKinematic = true;
            col.isTrigger = true;
        }
    }
}

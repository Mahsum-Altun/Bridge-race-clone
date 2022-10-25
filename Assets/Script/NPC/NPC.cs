using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class NPC : MonoBehaviour
{
    public enum NPCColor
    {
        White,
        Blue,
        Yellow,
    }
    public NPCColor nPCColor;
    public Transform[] wayPoints;
    public bool loopWayPoints;
    private NavMeshAgent agent;
    Animator animator;
    public GameObject brickTransformParent;
    int wayPoint = 0;
    bool coroutine = true;
    public Vector3 stairTransform;
    public Vector3 stairTransform2;
    public Vector3 stairTransform3;
    public Vector3 planeTransform;
    public Vector3 planeTransform2;
    public Vector3 planeTransform3;
    Vector3 npsWayParentTransform;
    public GameObject stairTransformGameObject;
    public GameObject npsWayParent;
    bool stairBool2 = false;
    bool stairBool3 = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    IEnumerator Patrol()
    {
        agent.SetDestination(wayPoints[wayPoint].position);
        while (true)
        {
            if (Vector3.Distance(wayPoints[wayPoint].position, transform.position) < 2)
            {
                if (wayPoint == wayPoints.Length - 1)
                {
                    if (loopWayPoints)
                    {
                        wayPoint = 0;
                        agent.SetDestination(wayPoints[0].position);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    wayPoint++;
                    agent.SetDestination(wayPoints[wayPoint].position);
                }
            }
            yield return new WaitForSeconds(.5f);
        }
        yield return null;
    }
    private void Update()
    {
        if (brickTransformParent.transform.childCount > 0)
        {
            stairTransform = new Vector3(stairTransform.x, stairTransform.y, stairTransform.z);
            stairTransformGameObject.transform.position = stairTransform;
            if (stairBool2 == true)
            {
                stairTransform2 = new Vector3(stairTransform2.x, stairTransform2.y, stairTransform2.z);
                stairTransformGameObject.transform.position = stairTransform2;
            }
            if (stairBool3 == true)
            {
                stairBool2 = false;
                stairTransform3 = new Vector3(stairTransform3.x, stairTransform3.y, stairTransform3.z);
                stairTransformGameObject.transform.position = stairTransform3;
            }
        }
        else if (brickTransformParent.transform.childCount == 0)
        {
            if (stairBool2 == false && stairBool3 == false)
            {
                planeTransform = new Vector3(planeTransform.x, planeTransform.y, planeTransform.z);
                stairTransformGameObject.transform.position = planeTransform;
            }
            if (stairBool2 == true && stairBool3 == false)
            {
                planeTransform2 = new Vector3(planeTransform2.x, planeTransform2.y, planeTransform2.z);
                stairTransformGameObject.transform.position = planeTransform2;
            }
            if (stairBool3 == true && stairBool2 == false)
            {
                planeTransform3 = new Vector3(planeTransform3.x, planeTransform3.y, planeTransform3.z);
                stairTransformGameObject.transform.position = planeTransform3;
            }
        }
        if (coroutine) { StartCoroutine("Patrol"); }
        if (agent.velocity.magnitude > 0.01f && Time.timeScale == 1)
        {
            animator.SetBool("Walk", true);
            if (brickTransformParent.transform.childCount > 0)
            {
                animator.SetBool("Run", true);
                animator.SetBool("Walk", false);
            }
            else
            {
                animator.SetBool("Run", false);
                animator.SetBool("Walk", true);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Yellow Brick")
        {
            if (nPCColor == NPCColor.Yellow)
            {
                agent.SetDestination(other.transform.position);

                coroutine = false;
            }
        }
        else if (other.gameObject.tag == "Blue Brick")
        {
            if (nPCColor == NPCColor.Blue)
            {
                agent.SetDestination(other.transform.position);
                coroutine = false;
            }
        }
        else if (other.gameObject.tag == "White Brick")
        {
            if (nPCColor == NPCColor.White)
            {
                agent.SetDestination(other.transform.position);

                coroutine = false;
            }
        }
        if (other.gameObject.tag == "BrickOpenSpawn")
        {
            stairBool2 = true;
            npsWayParentTransform = new Vector3(-0.23f, 5.33f, 44.77f);
            npsWayParent.transform.position = npsWayParentTransform;
        }
        if (other.gameObject.tag == "BrickOpenSpawn2")
        {
            stairBool3 = true;
            stairBool2 = false;
            npsWayParentTransform = new Vector3(0.15f, 7.26f, 89.93f);
            npsWayParent.transform.position = npsWayParentTransform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Yellow Brick")
        {
            if (nPCColor == NPCColor.Yellow)
            {
                coroutine = true;
            }
        }
        else if (other.gameObject.tag == "Blue Brick")
        {
            if (nPCColor == NPCColor.Blue)
            {
                coroutine = true;
            }
        }
        else if (other.gameObject.tag == "White Brick")
        {
            if (nPCColor == NPCColor.White)
            {
                coroutine = true;
            }
        }
    }
    public void IdleOpen()
    {
        agent.speed = 4f;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairPaintingYellow : MonoBehaviour
{

    GameObject brickTransformParent;
    public Material stairsMat;
    GameObject player;
    GameObject brickTransform;
    Vector3 newBrickTransform;
    public float brickPosY;
    Animator animator;
    private void Start()
    {
        player = GameObject.Find("Enemy Yellow");
        brickTransformParent = player.transform.GetChild(3).gameObject;
        brickTransform = player.transform.GetChild(4).gameObject;
        animator = transform.parent.GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Stairs" || other.gameObject.tag == "Red Stairs" || other.gameObject.tag == "Blue Stairs" || other.gameObject.tag == "White Stairs")
        {
            if (brickTransformParent.transform.childCount > 0)
            {
                other.gameObject.GetComponent<MeshRenderer>().enabled = true;
                other.gameObject.GetComponent<MeshRenderer>().material = stairsMat;
                other.gameObject.tag = "Yellow Stairs";
                Destroy(brickTransformParent.transform.GetChild(brickTransformParent.transform.childCount - 1).gameObject);
                newBrickTransform = new Vector3(brickTransform.transform.position.x, brickTransform.transform.position.y + brickPosY, brickTransform.transform.position.z);
                brickTransform.transform.position = newBrickTransform;
            }
        }
        if (other.gameObject.tag == "Ski")
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Run", false);
            animator.SetBool("Ski", true);
        }
        else
        {
            animator.SetBool("Ski", false);
        }
    }
}

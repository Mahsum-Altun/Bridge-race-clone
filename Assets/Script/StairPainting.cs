using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairPainting : MonoBehaviour
{
    GameObject brickTransformParent;
    CharacterController characterController;
    float originalStepOffset;
    public Material stairsMat;
    GameObject brickTransform;
    GameObject player;
    Vector3 newBrickTransform;
    public float brickPosY;
    public AudioSource audioSource;
    public AudioClip stairSound;
    private void Start()
    {
        player = GameObject.Find("Player");
        brickTransformParent = player.transform.GetChild(3).gameObject;
        characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        brickTransform = player.transform.GetChild(4).gameObject;
    }
    private void Update()
    {
        if (brickTransformParent.transform.childCount > 0)
        {
            characterController.stepOffset = originalStepOffset;
        }
        else
        {
            characterController.stepOffset = 0;
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Stairs" || hit.gameObject.tag == "Yellow Stairs" || hit.gameObject.tag == "Blue Stairs" || hit.gameObject.tag == "White Stairs")
        {
            if (brickTransformParent.transform.childCount > 0)
            {
                hit.gameObject.GetComponent<MeshRenderer>().enabled = true;
                hit.gameObject.GetComponent<MeshRenderer>().material = stairsMat;
                hit.gameObject.tag = "Red Stairs";
                Destroy(brickTransformParent.transform.GetChild(brickTransformParent.transform.childCount - 1).gameObject);
                newBrickTransform = new Vector3(brickTransform.transform.position.x, brickTransform.transform.position.y + brickPosY, brickTransform.transform.position.z);
                brickTransform.transform.position = newBrickTransform;
                audioSource.PlayOneShot(stairSound);
            }
        }
    }
}

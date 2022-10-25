using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickOpen : MonoBehaviour
{
    public GameObject red;
    public GameObject white;
    public GameObject blue;
    public GameObject yellow;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            red.SetActive(true);
        }
        if (other.gameObject.tag == "Enemy White")
        {
            white.SetActive(true);
        }
        if (other.gameObject.tag == "Enemy Blue")
        {
            blue.SetActive(true);
        }
        if (other.gameObject.tag == "Enemy Yellow")
        {
            yellow.SetActive(true);
        }
    }
}

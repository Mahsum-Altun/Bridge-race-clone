using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawning : MonoBehaviour
{
    public GameObject brick;

    void Start()
    {
        Instantiate(brick, transform.position, Quaternion.identity);
    }

    IEnumerator SpawnBrick()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(brick, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            StartCoroutine("SpawnBrick");
        }
    }

}

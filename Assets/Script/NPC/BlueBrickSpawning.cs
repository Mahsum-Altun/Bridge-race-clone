using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBrickSpawning : MonoBehaviour
{
    public GameObject brick;
    public GameObject redBrickSpawn;
    GameObject blueBrickSpawnParent;
    Collider col;

    void Start()
    {
        Instantiate(brick, transform.position, Quaternion.identity);
        blueBrickSpawnParent = GameObject.Find("Blue BrickSpawn Parent");
        col = GetComponent<Collider>();
    }
    private void Update()
    {
        if (AnimationManagerBlue.die == true)
        {
            blueBrickSpawnParent.SetActive(false);
            col.enabled = false;
            redBrickSpawn.SetActive(true);
        }
        else
        {
            blueBrickSpawnParent.SetActive(true);
            col.enabled = true;
            redBrickSpawn.SetActive(false);
        }
    }

    IEnumerator SpawnBrick()
    {
        yield return new WaitForSeconds(10f);
        Instantiate(brick, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Enemy Blue"))
        {
            StartCoroutine("SpawnBrick");
        }
    }
}

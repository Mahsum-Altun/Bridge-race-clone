using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBrickSpawning : MonoBehaviour
{
    public GameObject brick;
    public GameObject redBrickSpawn;
    GameObject yellowBrickSpawnParent;
    Collider col;

    void Start()
    {
        Instantiate(brick, transform.position, Quaternion.identity);
        yellowBrickSpawnParent = GameObject.Find("Yellow BrickSpawn Parent");
        col = GetComponent<Collider>();
    }
    private void Update()
    {
        if (AnimationManagerYellow.die == true)
        {
            yellowBrickSpawnParent.SetActive(false);
            col.enabled = false;
            redBrickSpawn.SetActive(true);
        }
        else
        {
            yellowBrickSpawnParent.SetActive(true);
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
        if (other.gameObject.tag == ("Enemy Yellow"))
        {
            StartCoroutine("SpawnBrick");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBrickSpawning : MonoBehaviour
{
    public GameObject brick;
    public GameObject redBrickSpawn;
    GameObject whiteBrickSpawnParent;
    Collider col;

    void Start()
    {
        Instantiate(brick, transform.position, Quaternion.identity);
        whiteBrickSpawnParent = GameObject.Find("White BrickSpawn Parent");
        col = GetComponent<Collider>();
    }
    private void Update()
    {
        if (AnimationManagerWhite.die == true)
        {
            whiteBrickSpawnParent.SetActive(false);
            col.enabled = false;
            redBrickSpawn.SetActive(true);
        }
        else
        {
            whiteBrickSpawnParent.SetActive(true);
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
        if (other.gameObject.tag == ("Enemy White"))
        {
            StartCoroutine("SpawnBrick");
        }
    }
}

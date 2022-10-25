using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public Transform explosionPrefab;
    public GameObject player;
    public GameObject playerDiePanel;
    public AudioSource drop;
    AdsManager adsManager;

    private void Start()
    {
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent.tag == "Player")
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;
            Instantiate(explosionPrefab, pos, rot);
            player.SetActive(false);
            playerDiePanel.SetActive(true);
            drop.Play();
            StartCoroutine("AdsControl");
        }
    }
    IEnumerator AdsControl()
    {
        yield return new WaitForSeconds(1f);
        adsManager.interstitial();
    }
}

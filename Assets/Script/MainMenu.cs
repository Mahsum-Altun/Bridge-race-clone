using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    AnimationManagerBlue animationManagerBlue;
    AdsManager adsManager;
    private void Start()
    {
        Time.timeScale = 0f;
        adsManager = GameObject.Find("AdsManager").GetComponent<AdsManager>();
    }
    public void Play()
    {
        Time.timeScale = 1f;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CameraFollow.win = false;
        adsManager.interstitial();
    }

}

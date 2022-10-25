using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string gameID = "4985461";
    private string Interstitial = "Interstitial_Android";
    private string Banner = "Banner_Android";
    public bool testMode = false;
    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameID, testMode);
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(Banner))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(Banner);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void interstitial()
    {
        if (Advertisement.IsReady(Interstitial))
        {
            Advertisement.Show(Interstitial);
        }
        else
        {
            Debug.Log("Interstitial not loaded");
        }
    }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if (showResult == ShowResult.Finished)
        {
            Debug.Log("Rewarded");
        }

        if (showResult == ShowResult.Skipped)
        {
            Debug.Log("Ads Skip");
        }

        if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("Error ADS Failed");
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        Debug.Log("Ads not loaded:" + message);
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ads started:" + placementId);
    }

    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ready" + placementId);
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

}

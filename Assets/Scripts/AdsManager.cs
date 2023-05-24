using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class AdsManager : MonoBehaviour
{
    public void ShowRewardedAd()
    {
#if UNITY_ADS
        const string RewardedPlacementId = "Rewarded_Android";

        if (Advertisement.IsReady(RewardedPlacementId))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(RewardedPlacementId, options);
        }
        else
        {
            Debug.Log(string.Format("Ads not ready for placement '{0}'", RewardedPlacementId));
            return;
        }

#endif
    }

    //private void HandleShowResult(ShowResult result)
    //{
#if UNITY_ADS
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
#endif
    //}
}
